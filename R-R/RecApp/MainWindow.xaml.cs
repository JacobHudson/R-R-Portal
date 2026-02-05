using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using Concierge.Shared;

namespace ReceptionFrontend;

public partial class MainWindow : Window
{
    private HubConnection _connection;
    public ObservableCollection<ServiceRequest> Requests { get; set; } = new();

    public MainWindow()
    {
        InitializeComponent();

        RequestsGrid.ItemsSource = Requests;

        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7095/requestHub")
            .WithAutomaticReconnect()
            .Build();

        _connection.On<ServiceRequest>("ReceiveNewRequest", (request) =>
        {
            Dispatcher.Invoke(() =>
            {
                Requests.Insert(0, request); 
                System.Media.SystemSounds.Beep.Play();
            });
        });

        StartConnection();
    }

    private async void StartConnection()
    {
        while (true)
        {
            try
            {
                await _connection.StartAsync();
                System.Diagnostics.Debug.WriteLine("Connected to SignalR hub.");
                MessageBox.Show("Connected to hub");
                break;
            }
            catch (Exception ex)
            {
                await Task.Delay(5000);
                MessageBox.Show($"Connection failed: {ex.Message}, reattempting...");
            }
        }
    }
}
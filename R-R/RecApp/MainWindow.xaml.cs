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
            .WithUrl("http://localhost:7095/requestHub")
            .WithAutomaticReconnect()
            .Build();

        _connection.On<ServiceRequest>("ReceiveNewRequest", (request) =>
        {
            Dispatcher.Invoke(() =>
            {
                Requests.Insert(0, request); 
                //System.Media.SystemSounds.Beep.Play(); // Alert the receptionist!
            });
        });

        StartConnection();
    }

    private async void StartConnection()
    {
        try
        {
            await _connection.StartAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Connection failed: {ex.Message}");
        }
    }
}
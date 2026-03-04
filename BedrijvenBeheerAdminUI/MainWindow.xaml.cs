using BedrijvenBL.Beheerders;
using BedrijvenBL.Domein;
using BedrijvenBL.DTOs;
using BedrijvenUtil;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BedrijvenBeheerAdminUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString;
        private string repoType;
        private BedrijfBeheerder bedrijfBeheerder;
        public MainWindow()
        {
            InitializeComponent();
            LeesConfig();
            bedrijfBeheerder = new BedrijfBeheerder(RepositoryFactory.GeefRepository(repoType,connectionString));
            DataGridBedrijven.ItemsSource = bedrijfBeheerder.GeefBedrijvenDTOs();
        }
        private void LeesConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = builder.Build();
            connectionString = config.GetConnectionString("ADOSQLConnection");
            repoType = config.GetSection("AppSettings")["RepoType"];
        }

        private void DataGridBedrijven_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridPersoneel.ItemsSource=bedrijfBeheerder.GeefPersoneelBedrijf(((BedrijfDTO)DataGridBedrijven.SelectedItem).Naam);
        }

        private void MenuItemNieuw_Click(object sender, RoutedEventArgs e)
        {
            NieuwPersoneelWindow w = new NieuwPersoneelWindow(bedrijfBeheerder,(int) ((BedrijfDTO)DataGridBedrijven.SelectedItem).Id);
            w.ShowDialog();
        }
    }
}
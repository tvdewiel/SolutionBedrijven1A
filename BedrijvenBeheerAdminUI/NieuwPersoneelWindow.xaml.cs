using BedrijvenBL.Beheerders;
using BedrijvenBL.Domein;
using BedrijvenBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BedrijvenBeheerAdminUI
{
    /// <summary>
    /// Interaction logic for NieuwPersoneelWindow.xaml
    /// </summary>
    public partial class NieuwPersoneelWindow : Window
    {
        private BedrijfBeheerder bedrijfBeheerder;
        private int bedrijfsId;
        public NieuwPersoneelWindow(BedrijfBeheerder bedrijfBeheerder,int bedrijfsId)
        {
            InitializeComponent();
            this.bedrijfBeheerder = bedrijfBeheerder;
            this.bedrijfsId = bedrijfsId;
        }

        private void ButtonOpslaan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Personeel personeel = new Personeel(TextBoxVoornaam.Text,
                    TextBoxFamilienaam.Text,
                    TextBoxEmail.Text,
                    new Adres(TextBoxWoonplaats.Text, TextBoxStraat.Text, int.Parse(TextBoxPostcode.Text), TextBoxHuisnummer.Text),
                    (DateTime)DatePickerGeboortedatum.SelectedDate);

                bedrijfBeheerder.VoegPersoneelToe(bedrijfsId,personeel);
            }
            catch(BedrijvenDomeinException ex)
            {
                MessageBox.Show(string.Join(',',ex.Errors),"Personeel aanmaken niet gelukt",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}

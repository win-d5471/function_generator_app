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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ivi;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Ivi.Visa.Interop.ResourceManager rm;
        Ivi.Visa.Interop.AccessMode accessMode;
        Ivi.Visa.Interop.IMessage msg;
        
        
        public MainWindow()
        {
            InitializeComponent();
        }
        public void connect()
        {

            rm = new Ivi.Visa.Interop.ResourceManager();
            accessMode = Ivi.Visa.Interop.AccessMode.NO_LOCK;
            // ファンクションジェネレータの製造番号
            string serial = TextBox_SerialNumber.Text;

            int timeOut = 0;
            string optionString = "";
            // デバイスと接続(USB)
            // WF1973の場合 "USB0::0x0D4A::0x000D::" としてください
            // WF1974の場合 "USB0::0x0D4A::0x000E::" としてください
            msg = (Ivi.Visa.Interop.IMessage)rm.Open(
                    "USB0::0x0D4A::0x000E::" + serial + "::INSTR",
                    accessMode,
                    timeOut,
                    optionString);

            // 周波数を5.0kHzに設定し、その値を問い合わせる
            msg.WriteString(":OUTP1:STAT OFF; " + "\n");
            msg.WriteString(":OUTP2:STAT OFF; " + "\n");
            msg.WriteString(":SOUR1:FUNC SIN \n");
            msg.WriteString(":SOUR2:FUNC SIN \n");

            Button1.Content = "切断";
            // デバイスを閉じる
            // msg.Close();


        }
    
     

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            connect();
            
        }


        //周波数　Hz
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            msg.WriteString(":SOUR1:FREQ "+ TextBox1.Text+";:SOUR1:FREQ?\n");
        }

        //電圧　V
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":SOUR1:VOLT "+TextBox2.Text+";:SOUR1:VOLT?\n");
        }

        //位相　°
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":SOUR1:PHAS " + TextBox3.Text + ";:SOUR1:PHAS?\n");
        }

        private void Button_offset_ch1_Click(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":SOUR1:VOLT:OFFS " + TextBox7.Text + ";:SOUR1:VOLT:OFFS?\n");
        }

        private void Button_output_ch1_Click(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":OUTP1:STAT ON; " +"\n");
        }

        private void Button_Hz_ch2_Click(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":SOUR2:FREQ " + TextBox4.Text + ";:SOUR2:FREQ?\n");
        }

        private void Button_offset_ch2_Click(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":SOUR2:VOLT:OFFS " + TextBox8.Text + ";:SOUR2:VOLT:OFFS?\n");
        }
    

        private void Button_volt_ch2_Click(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":SOUR2:VOLT " + TextBox5.Text + ";:SOUR2:VOLT?\n");
        }

        private void Button_phase_ch2_Click(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":SOUR2:PHAS " + TextBox6.Text + ";:SOUR2:PHAS?\n");
        }

        private void Button_output_ch2_Click(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":OUTP2:STAT ON; " + "\n");
        }

        private void Button_allsend_Click(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":SOUR1:FREQ " + TextBox1.Text + ";:SOUR1:FREQ?\n");
            msg.WriteString(":SOUR1:VOLT " + TextBox2.Text + ";:SOUR1:VOLT?\n");
            msg.WriteString(":SOUR1:PHAS " + TextBox3.Text + ";:SOUR1:PHAS?\n");
            msg.WriteString(":SOUR1:VOLT:OFFS " + TextBox7.Text + ";:SOUR1:VOLT:OFFS?\n");
            msg.WriteString(":SOUR2:FREQ " + TextBox4.Text + ";:SOUR2:FREQ?\n");
            msg.WriteString(":SOUR2:VOLT " + TextBox5.Text + ";:SOUR2:VOLT?\n");
            msg.WriteString(":SOUR2:PHAS " + TextBox6.Text + ";:SOUR2:PHAS?\n");
            msg.WriteString(":SOUR2:VOLT:OFFS " + TextBox8.Text + ";:SOUR2:VOLT:OFFS?\n");
        }

        private void Button_Manual_Input_Click(object sender, RoutedEventArgs e)
        {
            msg.WriteString(TextBox_Manual_Input.Text);
        }

        private void Button_output_Both_Click(object sender, RoutedEventArgs e)
        {
            msg.WriteString(":OUTP1:STAT ON; " + "\n");
            msg.WriteString(":OUTP2:STAT ON; " + "\n");
        }
    }

}

﻿/*
 *Ваши права на использование кода регулируются данной лицензией http://o-s-a.net/doc/license_simple_engine.pdf
*/

using System;
using System.Globalization;
using System.Windows;

namespace OsEngine.OsTrader.Panels.PanelsGui
{
    /// <summary>
    /// Логика взаимодействия для RsiContrtrend.xaml
    /// </summary>
    public partial class RsiContrtrendUi
    {
        private RsiContrtrend _strategy;
        public RsiContrtrendUi(RsiContrtrend strategy)
        {
            InitializeComponent();
            _strategy = strategy;

            ComboBoxRegime.Items.Add(BotTradeRegime.Off);
            ComboBoxRegime.Items.Add(BotTradeRegime.On);
            ComboBoxRegime.Items.Add(BotTradeRegime.OnlyClosePosition);
            ComboBoxRegime.Items.Add(BotTradeRegime.OnlyLong);
            ComboBoxRegime.Items.Add(BotTradeRegime.OnlyShort);
            ComboBoxRegime.SelectedItem = _strategy.Regime;

            TextBoxSlipage.Text = _strategy.Slipage.ToString(new CultureInfo("ru-RU"));

            TextBoxVolumeOne.Text = _strategy.VolumeFix.ToString();
            RsiUp.Text = _strategy.Upline.Value.ToString(new CultureInfo("ru-RU"));
            RsiDown.Text = _strategy.Downline.Value.ToString(new CultureInfo("ru-RU"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(TextBoxVolumeOne.Text) <= 0 ||  //BUG сравниваются одинаковые значения
                    Convert.ToInt32(RsiUp.Text) <= 0 || 
                    Convert.ToInt32(RsiDown.Text) <= 0 ||
                    Convert.ToDecimal(TextBoxSlipage.Text) < 0 ||
                    Convert.ToInt32(TextBoxVolumeOne.Text) <= 0)   //BUG сравниваются одинаковые значения
                {
                    throw new Exception("");
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("В одном из полей недопустимые значения. Процесс сохранения прерван");
                return;
            }

            Enum.TryParse(ComboBoxRegime.Text, true, out _strategy.Regime);
            _strategy.Slipage = Convert.ToDecimal(TextBoxSlipage.Text);
            _strategy.VolumeFix = Convert.ToInt32(TextBoxVolumeOne.Text);
            _strategy.Upline.Value = Convert.ToDecimal(RsiUp.Text);
            _strategy.Downline.Value = Convert.ToDecimal(RsiDown.Text);

            _strategy.Upline.Refresh();
            _strategy.Downline.Refresh();

            _strategy.Save();
            Close();
        }
    }
}
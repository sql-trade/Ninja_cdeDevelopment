#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
    public class cde_FourEMAs_Slope : Indicator
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description                 = @"4 EMAs_Slope  for autoTrading";
                Name                        = "cde_FourEMAs_Slope";
                Calculate                   = Calculate.OnPriceChange;
                IsOverlay                   = false;
                DisplayInDataBox            = true;
                DrawOnPricePanel            = true;
                DrawHorizontalGridLines     = true;
                DrawVerticalGridLines       = true;
                PaintPriceMarkers           = true;
                ScaleJustification          = NinjaTrader.Gui.Chart.ScaleJustification.Right;
                //Disable this property if your indicator requires custom values that cumulate with each new market data event. 
                //See Help Guide for additional information.
                IsSuspendedWhileInactive    = true;

                EMA1                        =   5 ;
                EMA2                        =  10 ;
                EMA3                        =  20 ;
                EMA4                        = 100 ;
                AddPlot(Brushes.DodgerBlue  , "EMA1_slope") ;
                AddPlot(Brushes.GreenYellow , "EMA2_slope") ;
                AddPlot(Brushes.Lime        , "EMA3_slope") ;
                AddPlot(Brushes.Red         , "EMA4_slope") ;
            }
            else if (State == State.Configure)
            {
            }
        }

        protected override void OnBarUpdate()
        {
            //Add your custom indicator logic here.
            //-- 1. "calculate" the current Values
            double ema1_Slope = Slope( EMA( EMA1 ) , 1, 0 ) ;
            double ema2_Slope = Slope( EMA( EMA2 ) , 1, 0 ) ;
            double ema3_Slope = Slope( EMA( EMA3 ) , 1, 0 ) ;
            double ema4_Slope = Slope( EMA( EMA4 ) , 1, 0 ) ;
            
            //-- 2. "plot" the Values
            EMA1_slope[0] = ema1_Slope ;
            EMA2_slope[0] = ema2_Slope ;
            EMA3_slope[0] = ema3_Slope ;
            EMA4_slope[0] = ema4_Slope ;
        }

        #region Properties
        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA1", Order=11, GroupName="1Parameters")]
        public int EMA1
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA2", Order=12, GroupName="1Parameters")]
        public int EMA2
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA3", Order=13, GroupName="1Parameters")]
        public int EMA3
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA4", Order=14, GroupName="1Parameters")]
        public int EMA4
        { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> EMA1_slope
        {
            get { return Values[0]; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> EMA2_slope
        {
            get { return Values[1]; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> EMA3_slope
        {
            get { return Values[2]; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> EMA4_slope
        {
            get { return Values[3]; }
        }
        #endregion

    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
    public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
    {
        private cde_FourEMAs_Slope[] cachecde_FourEMAs_Slope;
        public cde_FourEMAs_Slope cde_FourEMAs_Slope(int eMA1, int eMA2, int eMA3, int eMA4)
        {
            return cde_FourEMAs_Slope(Input, eMA1, eMA2, eMA3, eMA4);
        }

        public cde_FourEMAs_Slope cde_FourEMAs_Slope(ISeries<double> input, int eMA1, int eMA2, int eMA3, int eMA4)
        {
            if (cachecde_FourEMAs_Slope != null)
                for (int idx = 0; idx < cachecde_FourEMAs_Slope.Length; idx++)
                    if (cachecde_FourEMAs_Slope[idx] != null && cachecde_FourEMAs_Slope[idx].EMA1 == eMA1 && cachecde_FourEMAs_Slope[idx].EMA2 == eMA2 && cachecde_FourEMAs_Slope[idx].EMA3 == eMA3 && cachecde_FourEMAs_Slope[idx].EMA4 == eMA4 && cachecde_FourEMAs_Slope[idx].EqualsInput(input))
                        return cachecde_FourEMAs_Slope[idx];
            return CacheIndicator<cde_FourEMAs_Slope>(new cde_FourEMAs_Slope(){ EMA1 = eMA1, EMA2 = eMA2, EMA3 = eMA3, EMA4 = eMA4 }, input, ref cachecde_FourEMAs_Slope);
        }
    }
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
    public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
    {
        public Indicators.cde_FourEMAs_Slope cde_FourEMAs_Slope(int eMA1, int eMA2, int eMA3, int eMA4)
        {
            return indicator.cde_FourEMAs_Slope(Input, eMA1, eMA2, eMA3, eMA4);
        }

        public Indicators.cde_FourEMAs_Slope cde_FourEMAs_Slope(ISeries<double> input , int eMA1, int eMA2, int eMA3, int eMA4)
        {
            return indicator.cde_FourEMAs_Slope(input, eMA1, eMA2, eMA3, eMA4);
        }
    }
}

namespace NinjaTrader.NinjaScript.Strategies
{
    public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
    {
        public Indicators.cde_FourEMAs_Slope cde_FourEMAs_Slope(int eMA1, int eMA2, int eMA3, int eMA4)
        {
            return indicator.cde_FourEMAs_Slope(Input, eMA1, eMA2, eMA3, eMA4);
        }

        public Indicators.cde_FourEMAs_Slope cde_FourEMAs_Slope(ISeries<double> input , int eMA1, int eMA2, int eMA3, int eMA4)
        {
            return indicator.cde_FourEMAs_Slope(input, eMA1, eMA2, eMA3, eMA4);
        }
    }
}

#endregion

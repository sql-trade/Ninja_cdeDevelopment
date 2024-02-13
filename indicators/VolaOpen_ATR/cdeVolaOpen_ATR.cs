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
    public class cde_VolaOpen_ATR : Indicator
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description               = @"cde VolaOpen ATR shows a ""scalar Value"" in an infoBox.";
                Name                      = "cde_VolaOpen_ATR";     //-- Name Drawing Objects
                Calculate                 = Calculate.OnEachTick;
                IsOverlay                 = true;
                DisplayInDataBox          = true;
                DrawOnPricePanel          = true;
                DrawHorizontalGridLines   = true;
                DrawVerticalGridLines     = true;
                PaintPriceMarkers         = true;
                ScaleJustification        = NinjaTrader.Gui.Chart.ScaleJustification.Right;

                //Disable this property if your indicator requires custom values that cumulate with each new market data event. 
                //See Help Guide for additional information.

                IsSuspendedWhileInactive  = true;
                Period                    = 8 ;

            }
            else if (State == State.Configure)
            {
            }
        }

        protected override void OnBarUpdate()
        {
            //Add your custom indicator logic here.
            double atr = Math.Round(ATR(Period)[0], 2);
            double val = Math.Round( (atr * 4 * 0.05), 0);  // value * 4Ticks und davon 5%

            string infoTxt = " InfoBox                           \n"
                           + " ATR: " + atr.ToString("0.00")  + "\n"
                           + " Val: " + val.ToString("   0")  + "  "
            //----
            
            Draw.TextFixed( this
                          , Name   //--  refer line 34
                          , infoTxt
                          , TextPosition.TopRight
                          , Brushes.WhiteSmoke      //--  ChartControl.Properties.ChartText
                          , boxTextFont             //--  ChartControl.Properties.LabelFont
                          , Brushes.OrangeRed       //--  Brush outlineBrush  //-- Brushes.OrangeRed
                          , Brushes.Transparent     //--  Brush areaBrush  //--get value from "color Parameters"
                          , 100            //--  areaOpacity
                          , DashStyleHelper.Solid
                          , 1
                          , false     //--  bool isGlobal
                          , ""        //--  string templateName
                          );            
        }
        
        #region Properties
        [Range(1, int.MaxValue), NinjaScriptProperty]
        [Display(ResourceType = typeof(Custom.Resource), Name = "Period", GroupName = "1nput Parameters", Order = 0)]
        public int Period
        { get; set; }


        //----
        
        NinjaTrader.Gui.Tools.SimpleFont boxTextFont = new NinjaTrader.Gui.Tools.SimpleFont("Courier New", 10); 
        
        #endregion
        
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private cde_VolaOpen_ATR[] cachecde_VolaOpen_ATR;
		public cde_VolaOpen_ATR cde_VolaOpen_ATR(int period)
		{
			return cde_VolaOpen_ATR(Input, period);
		}

		public cde_VolaOpen_ATR cde_VolaOpen_ATR(ISeries<double> input, int period)
		{
			if (cachecde_VolaOpen_ATR != null)
				for (int idx = 0; idx < cachecde_VolaOpen_ATR.Length; idx++)
					if (cachecde_VolaOpen_ATR[idx] != null && cachecde_VolaOpen_ATR[idx].Period == period && cachecde_VolaOpen_ATR[idx].EqualsInput(input))
						return cachecde_VolaOpen_ATR[idx];
			return CacheIndicator<cde_VolaOpen_ATR>(new cde_VolaOpen_ATR(){ Period = period }, input, ref cachecde_VolaOpen_ATR);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.cde_VolaOpen_ATR cde_VolaOpen_ATR(int period)
		{
			return indicator.cde_VolaOpen_ATR(Input, period);
		}

		public Indicators.cde_VolaOpen_ATR cde_VolaOpen_ATR(ISeries<double> input , int period)
		{
			return indicator.cde_VolaOpen_ATR(input, period);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.cde_VolaOpen_ATR cde_VolaOpen_ATR(int period)
		{
			return indicator.cde_VolaOpen_ATR(Input, period);
		}

		public Indicators.cde_VolaOpen_ATR cde_VolaOpen_ATR(ISeries<double> input , int period)
		{
			return indicator.cde_VolaOpen_ATR(input, period);
		}
	}
}

#endregion

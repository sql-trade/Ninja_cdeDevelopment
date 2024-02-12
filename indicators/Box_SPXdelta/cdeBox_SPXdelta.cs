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
    public class cdeBox_SPXdelta : Indicator
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description               = @"cde Box SPX delta ""scalar Value"" in an infoBox.";
                Name                      = "cde_BOX_SPXdelta";     //-- Name Drawing Objects
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
                SPX_delta                 = 20 ;

            }
            else if (State == State.Configure)
            { 
            }
        }

        protected override void OnBarUpdate()
        {
            //Add your custom indicator logic here.
            double currES  = Close[0];
            double calcSPX = currES - SPX_delta ;
            string infoTxt = " InfoBox                                        \n" 
                           + " currES.: " +    currES.ToString(  "0.00 ") + " \n"
                           + " delta  : " + SPX_delta.ToString("  0.00 ") + " \n"
                           + " calcSPX: " +   calcSPX.ToString(  "0.00 ") + "   " 
                           ;

            //----
            
            Draw.TextFixed( this
                          , Name   //--  refer line 34
                          , infoTxt    
                          , TextPosition.TopRight
                          , Brushes.WhiteSmoke      //--  ChartControl.Properties.ChartText
                          , boxTextFont              //--  ChartControl.Properties.LabelFont
                          , Brushes.OrangeRed       //--  Brush outlineBrush  //-- Brushes.OrangeRed
                          , Brushes.Transparent     //--  Brush areaBrush  //--get value from "color Parameters"
                          , 100                     //--  areaOpacity
                          , DashStyleHelper.Solid
                          , 1
                          , false     //--  bool isGlobal
                          , ""        //--  string templateName
                          );            
        }
        
        #region Properties
        [Range(1, int.MaxValue), NinjaScriptProperty]
        [Display(ResourceType = typeof(Custom.Resource), Name = "SPX_delta", GroupName = "1nput Parameters", Order = 0)]
        public double SPX_delta
        { get; set; }

        //----
        
        NinjaTrader.Gui.Tools.SimpleFont boxTextFont = new NinjaTrader.Gui.Tools.SimpleFont("Courier New", 14); 
        
        #endregion
        
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private cdeBox_SPXdelta[] cachecdeBox_SPXdelta;
		public cdeBox_SPXdelta cdeBox_SPXdelta(double sPX_delta)
		{
			return cdeBox_SPXdelta(Input, sPX_delta);
		}

		public cdeBox_SPXdelta cdeBox_SPXdelta(ISeries<double> input, double sPX_delta)
		{
			if (cachecdeBox_SPXdelta != null)
				for (int idx = 0; idx < cachecdeBox_SPXdelta.Length; idx++)
					if (cachecdeBox_SPXdelta[idx] != null && cachecdeBox_SPXdelta[idx].SPX_delta == sPX_delta && cachecdeBox_SPXdelta[idx].EqualsInput(input))
						return cachecdeBox_SPXdelta[idx];
			return CacheIndicator<cdeBox_SPXdelta>(new cdeBox_SPXdelta(){ SPX_delta = sPX_delta }, input, ref cachecdeBox_SPXdelta);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.cdeBox_SPXdelta cdeBox_SPXdelta(double sPX_delta)
		{
			return indicator.cdeBox_SPXdelta(Input, sPX_delta);
		}

		public Indicators.cdeBox_SPXdelta cdeBox_SPXdelta(ISeries<double> input , double sPX_delta)
		{
			return indicator.cdeBox_SPXdelta(input, sPX_delta);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.cdeBox_SPXdelta cdeBox_SPXdelta(double sPX_delta)
		{
			return indicator.cdeBox_SPXdelta(Input, sPX_delta);
		}

		public Indicators.cdeBox_SPXdelta cdeBox_SPXdelta(ISeries<double> input , double sPX_delta)
		{
			return indicator.cdeBox_SPXdelta(input, sPX_delta);
		}
	}
}

#endregion

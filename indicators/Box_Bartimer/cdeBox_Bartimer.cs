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
    public class cdeBox_Bartimer : Indicator
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description               = @"cde Box Bartimer ""scalar Value"" in an infoBox.";
                Name                      = "cde_BOX_Bartimer";     //-- Name Drawing Objects
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

            }
            else if (State == State.Configure)
            { 
            }
        }

        protected override void OnBarUpdate()
        {
            //Add your custom indicator logic here.
            string currTime = " ... ";  //--  BarTimer().Value.ToString() ;
            string infoText = " InfoBox                    \n" 
                            + " currTime: " + currTime + " \n" 
                            ;

            //----
            
            Draw.TextFixed( this
                          , Name   //--  refer line 34
                          , infoText                  
                          , TextPosition.TopRight     
                          , Brushes.WhiteSmoke        //--  ChartControl.Properties.ChartText
                          , boxTextFont               //--  ChartControl.Properties.LabelFont
                          , Brushes.OrangeRed         //--  Brush outlineBrush  //-- Brushes.OrangeRed
                          , Brushes.Transparent       //--  Brush areaBrush  //--get value from "color Parameters"
                          , 100                       //--  areaOpacity
                          , DashStyleHelper.Solid     
                          , 1                         
                          , false     //--  bool isGlobal
                          , ""        //--  string templateName
                          );            
        }
        
        #region Properties
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
		private cdeBox_Bartimer[] cachecdeBox_Bartimer;
		public cdeBox_Bartimer cdeBox_Bartimer()
		{
			return cdeBox_Bartimer(Input);
		}

		public cdeBox_Bartimer cdeBox_Bartimer(ISeries<double> input)
		{
			if (cachecdeBox_Bartimer != null)
				for (int idx = 0; idx < cachecdeBox_Bartimer.Length; idx++)
					if (cachecdeBox_Bartimer[idx] != null &&  cachecdeBox_Bartimer[idx].EqualsInput(input))
						return cachecdeBox_Bartimer[idx];
			return CacheIndicator<cdeBox_Bartimer>(new cdeBox_Bartimer(), input, ref cachecdeBox_Bartimer);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.cdeBox_Bartimer cdeBox_Bartimer()
		{
			return indicator.cdeBox_Bartimer(Input);
		}

		public Indicators.cdeBox_Bartimer cdeBox_Bartimer(ISeries<double> input )
		{
			return indicator.cdeBox_Bartimer(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.cdeBox_Bartimer cdeBox_Bartimer()
		{
			return indicator.cdeBox_Bartimer(Input);
		}

		public Indicators.cdeBox_Bartimer cdeBox_Bartimer(ISeries<double> input )
		{
			return indicator.cdeBox_Bartimer(input);
		}
	}
}

#endregion

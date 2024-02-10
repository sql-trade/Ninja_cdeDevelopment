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
    public class cdeATRbox : Indicator
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description               = @"cde ATR box shows a ""scalar Value"" in an infoBox.";
                Name                      = "cde_ATR_box";     //-- Name Drawing Objects
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
                Period                    = 14;
                atrThreshold              = 3.5;
                adxThreshold              = 25;
                textcolor                 = Brushes.WhiteSmoke;   			
                background                = Brushes.Transparent;  //-- Brushes.Transparent
				outline                   = Brushes.OrangeRed;

            }
            else if (State == State.Configure)
            {
            }
        }

        protected override void OnBarUpdate()
        {
            //Add your custom indicator logic here.
            double atr = Math.Round(ATR(14)[0], 2);
            double adx = Math.Round(ADX(14)[0], 2);

            
            Draw.TextFixed( this
                          , Name   //--  refer line 34
                          , " InfoBox: \n"      
                          + " ATR: " + atr.ToString()  + "    \n"
                          + " ADX: " + adx.ToString()  + "    " 
                          , TextPosition.TopRight
                          , textcolor      //--  ChartControl.Properties.ChartText
                          , boxTextFont    //--  ChartControl.Properties.LabelFont
                          , outline        //--  Brush outlineBrush  //-- Brushes.OrangeRed
                          , background     //--  Brush areaBrush  //--get value from "color Parameters"
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

        [Range(1, double.MaxValue), NinjaScriptProperty]
        [Display(ResourceType = typeof(Custom.Resource), Name = "ATR Threshold", GroupName = "1nput Parameters", Order = 1)]
        public double atrThreshold
        { get; set; }
        
        [Range(1, double.MaxValue), NinjaScriptProperty]
        [Display(ResourceType = typeof(Custom.Resource), Name = "ADX Threshold", GroupName = "1nput Parameters", Order = 2)]
        public double adxThreshold
        { get; set; }        


		
		[XmlIgnore]
        [Display(Name="textcolor", Order = 4, GroupName = "color Parameters")]
        public Brush textcolor
        { get; set; }		

//--        [XmlIgnore]		
        [Display(Name="background", Order = 5, GroupName = "color Parameters")]
        public Brush background
        { get; set; }

//--        [XmlIgnore]		
		[Display(Name="outline", Order = 6, GroupName = "color Parameters")]
        public Brush outline
        { get; set; }

		
        [Browsable(false)]
        public string textcolorSerializable
        {
            get { return Serialize.BrushToString(textcolor); }
            set { textcolor = Serialize.StringToBrush(value); }
        }		
		
        public string backgroundSerializable
        {
            get { return Serialize.BrushToString(background); }
            set { background = Serialize.StringToBrush(value); }
        }

        public string outlineSerializable
        {
            get { return Serialize.BrushToString(outline); }
            set { outline = Serialize.StringToBrush(value); }
        }		
		
		
		NinjaTrader.Gui.Tools.SimpleFont boxTextFont = new NinjaTrader.Gui.Tools.SimpleFont("Courier New", 10); 
		
        #endregion
        
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private cdeATRbox[] cachecdeATRbox;
		public cdeATRbox cdeATRbox(int period, double atrThreshold, double adxThreshold)
		{
			return cdeATRbox(Input, period, atrThreshold, adxThreshold);
		}

		public cdeATRbox cdeATRbox(ISeries<double> input, int period, double atrThreshold, double adxThreshold)
		{
			if (cachecdeATRbox != null)
				for (int idx = 0; idx < cachecdeATRbox.Length; idx++)
					if (cachecdeATRbox[idx] != null && cachecdeATRbox[idx].Period == period && cachecdeATRbox[idx].atrThreshold == atrThreshold && cachecdeATRbox[idx].adxThreshold == adxThreshold && cachecdeATRbox[idx].EqualsInput(input))
						return cachecdeATRbox[idx];
			return CacheIndicator<cdeATRbox>(new cdeATRbox(){ Period = period, atrThreshold = atrThreshold, adxThreshold = adxThreshold }, input, ref cachecdeATRbox);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.cdeATRbox cdeATRbox(int period, double atrThreshold, double adxThreshold)
		{
			return indicator.cdeATRbox(Input, period, atrThreshold, adxThreshold);
		}

		public Indicators.cdeATRbox cdeATRbox(ISeries<double> input , int period, double atrThreshold, double adxThreshold)
		{
			return indicator.cdeATRbox(input, period, atrThreshold, adxThreshold);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.cdeATRbox cdeATRbox(int period, double atrThreshold, double adxThreshold)
		{
			return indicator.cdeATRbox(Input, period, atrThreshold, adxThreshold);
		}

		public Indicators.cdeATRbox cdeATRbox(ISeries<double> input , int period, double atrThreshold, double adxThreshold)
		{
			return indicator.cdeATRbox(input, period, atrThreshold, adxThreshold);
		}
	}
}

#endregion

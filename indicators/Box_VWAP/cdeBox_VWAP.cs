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
    public class cdeBox_Box_VWAP : Indicator
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description               = @"cde Box shows the ""VWAP values"" in an infoBox.";
                Name                      = "cde_Box_VWAP";     //-- Name Drawing Objects
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
                textcolor                 = Brushes.WhiteSmoke;               

            }
            else if (State == State.Configure)
            {
            }
        }

        protected override void OnBarUpdate()
        {
            //Add your custom indicator logic here.

            double vwap = OrderFlowVWAP(VWAPResolution.Standard, TradingHours.String2TradingHours("CME US Index Futures RTH"), VWAPStandardDeviations.Three, 1, 2, 3).VWAP[0];


            
            Draw.TextFixed( this
                          , Name   //--  refer line 34
                          , " Volume VWAP ."          + "\n"      
                          + " VWAP: " + vwap.ToString("0.00")  + "\n"

                          , TextPosition.TopRight
                          , textcolor      //--  ChartControl.Properties.ChartText
                          , boxTextFont    //--  ChartControl.Properties.LabelFont
                          , Brushes.OrangeRed       //--  outline 
                          , Brushes.Transparent     //--  background      
                          , 100                     //--  areaOpacity
                          , DashStyleHelper.Solid
                          , 1
                          , false     //--  bool isGlobal
                          , ""        //--  string templateName
                          );            
        }
        
        #region Properties
        [XmlIgnore]
        [Display(Name="textcolor", Order = 0, GroupName = "color Parameters")]
        public Brush textcolor
        { get; set; }        

        [Browsable(false)]
        public string textcolorSerializable
        {
            get { return Serialize.BrushToString(textcolor); }
            set { textcolor = Serialize.StringToBrush(value); }
        }        
        
        //---------
        NinjaTrader.Gui.Tools.SimpleFont boxTextFont = new NinjaTrader.Gui.Tools.SimpleFont("Courier New", 10); 
        
        #endregion
        
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
    public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
    {
        private cdeBox_Box_VWAP[] cachecdeBox_Box_VWAP;
        public cdeBox_Box_VWAP cdeBox_Box_VWAP()
        {
            return cdeBox_Box_VWAP(Input);
        }

        public cdeBox_Box_VWAP cdeBox_Box_VWAP(ISeries<double> input)
        {
            if (cachecdeBox_Box_VWAP != null)
                for (int idx = 0; idx < cachecdeBox_Box_VWAP.Length; idx++)
                    if (cachecdeBox_Box_VWAP[idx] != null &&  cachecdeBox_Box_VWAP[idx].EqualsInput(input))
                        return cachecdeBox_Box_VWAP[idx];
            return CacheIndicator<cdeBox_Box_VWAP>(new cdeBox_Box_VWAP(), input, ref cachecdeBox_Box_VWAP);
        }
    }
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
    public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
    {
        public Indicators.cdeBox_Box_VWAP cdeBox_Box_VWAP()
        {
            return indicator.cdeBox_Box_VWAP(Input);
        }

        public Indicators.cdeBox_Box_VWAP cdeBox_Box_VWAP(ISeries<double> input )
        {
            return indicator.cdeBox_Box_VWAP(input);
        }
    }
}

namespace NinjaTrader.NinjaScript.Strategies
{
    public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
    {
        public Indicators.cdeBox_Box_VWAP cdeBox_Box_VWAP()
        {
            return indicator.cdeBox_Box_VWAP(Input);
        }

        public Indicators.cdeBox_Box_VWAP cdeBox_Box_VWAP(ISeries<double> input )
        {
            return indicator.cdeBox_Box_VWAP(input);
        }
    }
}

#endregion

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
    public class cdeInfoBox : Indicator
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description               = @"cde ""InfoBox"" shows current Values for further calculations.";
                Name                      = "cde_InfoBox";
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
                
                col_0   = "c__0";
                col_1   = "c__1";
                col_2   = "c__2";

                row_1   = " r_1";
                row_2   = " r_2";
                row_3   = " r_3";
                
            }
            else if (State == State.Configure)
            {
            }
        }

        protected override void OnBarUpdate()
        {
            //Add your custom indicator logic here.
            
            // calc all values
            double  c0r1  = 12.5;
            double  c0r2  = 14.5;
            double  c0r3  = 16.5;

            double  c1r1  = 11.2;
            double  c1r2  = 11.4;
            double  c1r3  = 11.6;

            double  c2r1  = 10.1;
            double  c2r2  = 10.5;
            double  c2r3  = 10.7;

//--        double  c3r1  = 12.1;
//--        double  c3r2  = 14.1;
//--        double  c3r3  = 16.1;

            // end calc

            Draw.TextFixed( this
                          , Name   //--  refer line 34
                          , " current values: \n"
                          + "                 \n"
                          +     "     | "    + col_2        + "  | " + col_1           + "  | " + col_0            + " \n"
                          + row_1 + " | " + c2r1.ToString() + "  | " + c1r1.ToString() + "  | " + c0r1.ToString()  + " \n"
                          + row_2 + " | " + c2r2.ToString() + "  | " + c1r2.ToString() + "  | " + c0r2.ToString()  + " \n"
                          + row_3 + " | " + c2r3.ToString() + "  | " + c1r3.ToString() + "  | " + c0r3.ToString()  + " \n"

                          , TextPosition.TopRight
                          , Brushes.WhiteSmoke    //--  textcolor             //--  ChartControl.Properties.ChartText
                          , boxTextFont           //--  ChartControl.Properties.LabelFont
                          , Brushes.OrangeRed     //--  Brush outlineBrush    //--  Brushes.OrangeRed
                          , Brushes.Transparent   //--  background            //--  Brush areaBrush  
                          , 100                   //--  areaOpacity
                          , DashStyleHelper.Solid
                          , 1
                          , false     //--  bool isGlobal
                          , ""        //--  string templateName
                          );

        }
        
        #region Properties
        
        [Display(ResourceType = typeof(Custom.Resource), Name = "col_0", GroupName = "1nput Parameters", Order = 0)]
        public string col_0    { get; set; }

        [Display(ResourceType = typeof(Custom.Resource), Name = "col_1", GroupName = "1nput Parameters", Order = 1)]
        public string col_1    { get; set; }

        [Display(ResourceType = typeof(Custom.Resource), Name = "col_2", GroupName = "1nput Parameters", Order = 2)]
        public string col_2    { get; set; }


        [Display(ResourceType = typeof(Custom.Resource), Name = "row_1", GroupName = "1nput Parameters", Order = 3)]
        public string row_1    { get; set; }

        [Display(ResourceType = typeof(Custom.Resource), Name = "row_2", GroupName = "1nput Parameters", Order = 4)]
        public string row_2    { get; set; }

        [Display(ResourceType = typeof(Custom.Resource), Name = "row_3", GroupName = "1nput Parameters", Order = 5)]
        public string row_3    { get; set; }

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
        private cdeInfoBox[] cachecdeInfoBox;
        public cdeInfoBox cdeInfoBox()
        {
            return cdeInfoBox(Input);
        }

        public cdeInfoBox cdeInfoBox(ISeries<double> input)
        {
            if (cachecdeInfoBox != null)
                for (int idx = 0; idx < cachecdeInfoBox.Length; idx++)
                    if (cachecdeInfoBox[idx] != null &&  cachecdeInfoBox[idx].EqualsInput(input))
                        return cachecdeInfoBox[idx];
            return CacheIndicator<cdeInfoBox>(new cdeInfoBox(), input, ref cachecdeInfoBox);
        }
    }
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
    public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
    {
        public Indicators.cdeInfoBox cdeInfoBox()
        {
            return indicator.cdeInfoBox(Input);
        }

        public Indicators.cdeInfoBox cdeInfoBox(ISeries<double> input )
        {
            return indicator.cdeInfoBox(input);
        }
    }
}

namespace NinjaTrader.NinjaScript.Strategies
{
    public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
    {
        public Indicators.cdeInfoBox cdeInfoBox()
        {
            return indicator.cdeInfoBox(Input);
        }

        public Indicators.cdeInfoBox cdeInfoBox(ISeries<double> input )
        {
            return indicator.cdeInfoBox(input);
        }
    }
}

#endregion

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
using NinjaTrader.NinjaScript.Indicators;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Strategies in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Strategies
{
    public class cde_sunny01 : Strategy
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description                    = @"v1.04 // example from sunny (3.9)  /  Volume and ATR";
                Name                           = "cde_sunny01";
                Calculate                      = Calculate.OnBarClose;
                EntriesPerDirection            = 1;
                EntryHandling                  = EntryHandling.AllEntries;
                IsExitOnSessionCloseStrategy   = true;
                ExitOnSessionCloseSeconds      = 30;
                IsFillLimitOnTouch             = false;
                MaximumBarsLookBack            = MaximumBarsLookBack.TwoHundredFiftySix;
                OrderFillResolution            = OrderFillResolution.Standard;
                Slippage                       = 0;
                StartBehavior                  = StartBehavior.WaitUntilFlat;
                TimeInForce                    = TimeInForce.Gtc;
                TraceOrders                    = false;
                RealtimeErrorHandling          = RealtimeErrorHandling.IgnoreAllErrors;  //-- original RealtimeErrorHandling.StopCancelClose;
                StopTargetHandling             = StopTargetHandling.PerEntryExecution;
                BarsRequiredToTrade            = 20;
                // Disable this property for performance gains in Strategy Analyzer optimizations
                // See the Help Guide for additional information
                IsInstantiatedOnEachOptimizationIteration    = true;

                //--  FourEMAs configValues
                EMA1            =   5 ;
                EMA2            =  10 ;
                EMA3            =  20 ;
                EMA4            = 100 ;

                RSIupperL       =  80 ;
                RSIlowerL       =  20 ;
                RSIperiod       =  14 ;
                RSIsmooth       =   3 ;

                //--  Trade config with TP
                Quantity        =     1 ;
                PT_ticks        =   800 ;  //-- PT = ProfitTarget
                SL_ticks        =    16 ;  //-- SL = StopLoss
                SL_isTrailing   =  true ;
                
                Long_desc       = @"Long_Entry_v1.04";
                Short_desc      = @"Short_Entry_v1.04";
                StartTime       = DateTime.Parse("15:30", System.Globalization.CultureInfo.InvariantCulture);
                EndTime         = DateTime.Parse("21:40", System.Globalization.CultureInfo.InvariantCulture);
                
                AddPlot(new Stroke(Brushes.OrangeRed, 2), PlotStyle.TriangleUp  , "Long_Entry_plot" );
                AddPlot(new Stroke(Brushes.OrangeRed, 2), PlotStyle.TriangleDown, "Short_Entry_plot");
            }
            else if (State == State.Configure)
            {
                //-- Long
                SetProfitTarget(Long_desc, CalculationMode.Ticks, PT_ticks);
                if ( SL_isTrailing ) //-- default = false
                   SetStopLoss( Long_desc, CalculationMode.Ticks, SL_ticks, false);
                else
                   SetTrailStop(Long_desc, CalculationMode.Ticks, SL_ticks, false);  //-- true | false --> isMIT : Sets the StopLoss  as a "market-if-touched" order
                
                //-- Short
                SetProfitTarget(Short_desc, CalculationMode.Ticks, PT_ticks);
                if ( SL_isTrailing ) //-- default = false
                   SetStopLoss( Short_desc, CalculationMode.Ticks, SL_ticks, false);
                else
                   SetTrailStop(Short_desc, CalculationMode.Ticks, SL_ticks, false);
            }
        }

        protected override void OnBarUpdate()
        {
            //Add your custom strategy logic here.
            bool noTrades = Position.MarketPosition == MarketPosition.Flat;  //-- enter Long/Short only from "flat"-Status
            
            if (CurrentBar < 100) return;  //-- too less bars --> return;
            
//            if (   (Times[0][0].TimeOfDay < auto_StartTime.TimeOfDay)  //-- only on "Main" TradingHours
//                || (Times[0][0].TimeOfDay > auto_EndTime.TimeOfDay)
//               )       return;        

            //-- conditions "Long"
            if (    noTrades  
                &&  CrossAbove( EMA(EMA1), EMA(EMA2), 1)
                &&  RSI( RSIperiod, RSIsmooth )[0] <= RSIupperL   // RSI(period, smooth)[0]   //-- period = Number of bars used in the calculation, smooth = smoothing period
               )
                EnterLong( Quantity, Long_desc );
                //--Draw.ArrowUp(this, @"arrowUp_info", false, 0, 0, Brushes.Green);
            
            //-- conditions "Short"
            if (    noTrades  
                &&  CrossBelow( EMA(EMA1), EMA(EMA2), 1)
                &&  RSI( RSIperiod, RSIsmooth )[0] >= RSIlowerL
               )
                EnterShort( Quantity, Short_desc );
                //--Draw.ArrowDown(this, @"arrowDown_info", false, 0, 0, Brushes.Red);
            
            
        }

        #region Properties
        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA1", Order=1, GroupName="Parameters")]
        public int EMA1
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA2", Order=2, GroupName="Parameters")]
        public int EMA2
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA3", Order=3, GroupName="Parameters")]
        public int EMA3
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA4", Order=4, GroupName="Parameters")]
        public int EMA4
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="RSIupperL", Order=5, GroupName="Parameters")]
        public int RSIupperL
        { get; set; }		
		
        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="RSIlowerL", Order=6, GroupName="Parameters")]
        public int RSIlowerL
        { get; set; }		

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="RSIperiod", Order=7, GroupName="Parameters")]
        public int RSIperiod
        { get; set; }		

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="RSIsmooth", Order=8, GroupName="Parameters")]
        public int RSIsmooth
        { get; set; }		
		
        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="Quantity", Order=9, GroupName="Parameters")]
        public int Quantity
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="PT_ticks", Order=10, GroupName="Parameters")]
        public int PT_ticks
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="SL_ticks", Order=11, GroupName="Parameters")]
        public int SL_ticks
        { get; set; }
        
        [NinjaScriptProperty]
        [Display(Name="SL_isTrailing", Order=12, GroupName="Parameters")]
        public bool SL_isTrailing
        { get; set; }        

        [NinjaScriptProperty]
        [Display(Name="Long_desc", Order=13, GroupName="Parameters")]
        public string Long_desc
        { get; set; }

        [NinjaScriptProperty]
        [Display(Name="Short_desc", Order=14, GroupName="Parameters")]
        public string Short_desc
        { get; set; }

        [NinjaScriptProperty]
        [PropertyEditor("NinjaTrader.Gui.Tools.TimeEditorKey")]
        [Display(Name="StartTime", Order=15, GroupName="Parameters")]
        public DateTime StartTime
        { get; set; }

        [NinjaScriptProperty]
        [PropertyEditor("NinjaTrader.Gui.Tools.TimeEditorKey")]
        [Display(Name="EndTime", Order=16, GroupName="Parameters")]
        public DateTime EndTime
        { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> Long_Entry_plot
        {
            get { return Values[0]; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> Short_Entry_plot
        {
            get { return Values[1]; }
        }
        #endregion

    }
}

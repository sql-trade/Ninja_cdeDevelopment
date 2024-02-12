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
	public class cde_ThreeEMAs : Indicator
	{
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description		 = @"3 EMAs at once  instead of 3 (Standard) EMAs ";
				Name			 = "cde_ThreeEMAs";
				Calculate		 = Calculate.OnBarClose;
				IsOverlay		 = true;
				DisplayInDataBox	 = true;
				DrawOnPricePanel	 = true;
				DrawHorizontalGridLines	 = true;
				DrawVerticalGridLines	 = true;
				PaintPriceMarkers	 = true;
				ScaleJustification	 = NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive = true;

				EMA1			  =   8 ;
				EMA2			  =  20 ;
				EMA3			  = 110 ;
				AddPlot(Brushes.Green    , "EMA1_plot") ;
				AddPlot(Brushes.Orange   , "EMA2_plot") ;
				AddPlot(Brushes.OrangeRed, "EMA3_plot") ;
			}
			else if (State == State.Configure)
			{
			}
		}

		protected override void OnBarUpdate()
		{
			//Add your custom indicator logic here.
			//-- 1. "calculate" the current Values
			double ema1 = EMA(Close, EMA1)[0] ;
			double ema2 = EMA(Close, EMA2)[0] ;
			double ema3 = EMA(Close, EMA3)[0] ;		
			
			//-- 2. "plot" the Values
			EMA1_plot[0] = ema1 ;
			EMA2_plot[0] = ema2 ;
			EMA3_plot[0] = ema3 ;
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

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> EMA1_plot
		{
			get { return Values[0]; }
		}

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> EMA2_plot
		{
			get { return Values[1]; }
		}

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> EMA3_plot
		{
			get { return Values[2]; }
		}
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private cde_ThreeEMAs[] cachecde_ThreeEMAs;
		public cde_ThreeEMAs cde_ThreeEMAs(int eMA1, int eMA2, int eMA3)
		{
			return cde_ThreeEMAs(Input, eMA1, eMA2, eMA3);
		}

		public cde_ThreeEMAs cde_ThreeEMAs(ISeries<double> input, int eMA1, int eMA2, int eMA3)
		{
			if (cachecde_ThreeEMAs != null)
				for (int idx = 0; idx < cachecde_ThreeEMAs.Length; idx++)
					if (cachecde_ThreeEMAs[idx] != null && cachecde_ThreeEMAs[idx].EMA1 == eMA1 && cachecde_ThreeEMAs[idx].EMA2 == eMA2 && cachecde_ThreeEMAs[idx].EMA3 == eMA3 && cachecde_ThreeEMAs[idx].EqualsInput(input))
						return cachecde_ThreeEMAs[idx];
			return CacheIndicator<cde_ThreeEMAs>(new cde_ThreeEMAs(){ EMA1 = eMA1, EMA2 = eMA2, EMA3 = eMA3 }, input, ref cachecde_ThreeEMAs);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.cde_ThreeEMAs cde_ThreeEMAs(int eMA1, int eMA2, int eMA3)
		{
			return indicator.cde_ThreeEMAs(Input, eMA1, eMA2, eMA3);
		}

		public Indicators.cde_ThreeEMAs cde_ThreeEMAs(ISeries<double> input , int eMA1, int eMA2, int eMA3)
		{
			return indicator.cde_ThreeEMAs(input, eMA1, eMA2, eMA3);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.cde_ThreeEMAs cde_ThreeEMAs(int eMA1, int eMA2, int eMA3)
		{
			return indicator.cde_ThreeEMAs(Input, eMA1, eMA2, eMA3);
		}

		public Indicators.cde_ThreeEMAs cde_ThreeEMAs(ISeries<double> input , int eMA1, int eMA2, int eMA3)
		{
			return indicator.cde_ThreeEMAs(input, eMA1, eMA2, eMA3);
		}
	}
}

#endregion

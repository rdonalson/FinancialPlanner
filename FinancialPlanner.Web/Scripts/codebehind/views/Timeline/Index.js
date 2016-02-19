/* #######################################################################################
 * Javascript code-behind page for the Timeline view
 * ######################################################################################*/
/* ======================================================================================
 * Dataset and Chart Configurations
 * ======================================================================================*/
/* Declaration & Initialization */
var timeUnitSize = {
// These are intended for bar width only
    "day": 24 * 60 * 60 * 800,
    "week": 7 * 24 * 60 * 60 * 800,
    "month": 30 * 24 * 60 * 60 * 800,
    "quarter": 90 * 24 * 60 * 60 * 800,
    "year": 365.2425 * 24 * 60 * 60 * 800
};
var standardDay = 24 * 60 * 60 * 1000;
var MainList = new Array(),
    RunningTotalList = new Array(),
    CreditList = new Array(),
    DebitList = new Array(),
    NetAmount = new Array(),
    dailyBarWidth = timeUnitSize.day,
    choiceContainer;
/* Datasets */
var dsRunningTotal = [
    {
        label: "Running Total",
        data: RunningTotalList,
        color: "blue",
        points: { symbol: "triangle", fillColor: "blue", show: true },
        lines: { show: true }
    }
];
var dsCredits = {
    label: "Credits",
    data: CreditList,
    color: "green",
    bars: {
        show: true,
        align: "center",
        barWidth: dailyBarWidth,
        lineWidth: 1
    }
};
var dsDebits = {
    label: "Debits",
    data: DebitList,
    color: "red",
    bars: {
        show: true,
        align: "center",
        barWidth: dailyBarWidth,
        lineWidth: 1
    }
};
var dsNet = {
    label: "Net Daily",
    data: NetAmount,
    color: "black",
    points: {
        symbol: "circle",
        radius: 1,
        show: true
    },
    lines: {
        show: true,
        lineWidth: .5
    }
};
/* Overall container for "dsCredits", "dsDebits" & "dsNet" */
var dsCDND = [
    [], [], []
];

/* Chart Options */
var options = {
    xaxis: {
        mode: "time",
        tickLength: 0,
        rotateTicks: 75,
        labelWidth: 50,
        axisLabelAlign: "center",
        axisLabelUseCanvas: true,
        axisLabelFontSizePixels: 10,
        axisLabelFontFamily: "Verdana, Arial",
        axisLabelPadding: 10,
        color: "black"
    },
    yaxes: [
        {
            tickFormatter: function(yvalue) {
                return FormatNumber(yvalue, 0, "", ",", ".");
            },
            position: "left",
            color: "black",
            labelWidth: 50,
            axisLabelUseCanvas: true,
            axisLabelFontSizePixels: 12,
            axisLabelFontFamily: "Verdana, Arial",
            axisLabelPadding: 3
        }
    ],
    legend: {
        noColumns: 1,
        labelBoxBorderColor: "#000000",
        position: "nw"
    },
    grid: {
        hoverable: true,
        clickable: true,
        borderWidth: 3,
        backgroundColor: { colors: ["#ffffff", "#EDF5FF"] },
        margin: {
            top: 0,
            left: 0,
            bottom: 10,
            right: 0
        }
    }
};

/* ======================================================================================
 * Initialize page
 * ======================================================================================*/
function InitializeTimelineChart(model)
{
    choiceContainer = $("#choicesCDND");
    choiceContainer.find("input").click(PlotCDND);
    if (model != undefined)
        GetData(model);
}


/* ======================================================================================
 * Utility Functions
 * ======================================================================================*/
/* ---------------------------------------------------------------------------------------
 * This function gets the base data from the "spCreateLedgerReadout" database procedure by  
 * calling the associated controller function "GetLedgerReadout", which in turn calls its  
 * respective repository function. 
 * Then take the data and construct the "PrimaryList" which is distinct list of the 
 * overall and summary fields which are repeated for a given date such as 
 * Date, Credit Summary, Debit Summary, Net Daily and RunningTotal. 
 * Next construct the "DetailList" of Credit and Detail items which consist of the 
 * fields Date, ItemType (1 "Credit" or 2 "Debit"), Period (period type), Name (Item Name) 
 * and Item Amount.
 * -------------------------------------------------------------------------------------*/
function GetData(model) {
    var viewModel = {
        timeFrameBegin: $("#timeFrameBegin").val(),
        timeFrameEnd: $("#timeFrameEnd").val()
    };

    MainList = model.Result;

    /* Diagnostic */
    //viewModel.timeFrameBegin = "1/1/2016";
    //viewModel.timeFrameEnd = "3/1/2016";

    /* In days */
    var dailyCutoff = 60;
    var weeklyCutoff = 360;
    var monthlyCutoff = 1090;
    var quarterlyCutoff = 2850;

    AutoAdjustBarWidth(
        viewModel.timeFrameBegin,
        viewModel.timeFrameEnd,
        dailyCutoff,
        weeklyCutoff,
        monthlyCutoff,
        quarterlyCutoff,
        dsCredits,
        dsDebits
    );
    
    RunningTotalList.length = 0;
    CreditList.length = 0;
    DebitList.length = 0;
    NetAmount.length = 0;
    $.each(MainList, function(index, value) {
        /* Running Total */
        var rt = [];
        rt.push(GetTime(value.WDate), value.RunningTotal); // Date, Running Total
        RunningTotalList.push(rt);
        /* Credits */
        var c = [];
        c.push(GetTime(value.WDate), value.CreditSummary); // Date, Credit Summary
        CreditList.push(c);
        /* Debits */
        var d = [];
        d.push(GetTime(value.WDate), value.DebitSummary); // Date, Debit Summary
        DebitList.push(d);
        /* Net Daily Amounts */
        var nda = [];
        nda.push(GetTime(value.WDate), value.Net); // Date, Net Daily Amount
        NetAmount.push(nda);
    });
    PlotRT();
    PlotCDND();
}

/* ---------------------------------------------------------------------------------------
 * Auto adjust the bar width by determining the time span in days and setting the 
 * bar width according introduced cutoff values.
 * -------------------------------------------------------------------------------------*/
function AutoAdjustBarWidth(
    timeFrameBegin,
    timeFrameEnd,
    dailyCutoff,
    weeklyCutoff,
    monthlyCutoff,
    quarterlyCutoff,
    barFirst,
    barSecond) {

    var start = new Date(timeFrameBegin);
    var end = new Date(timeFrameEnd);
    var timespan = end - start;
    var days = timespan / standardDay;

    /* Time frame In days */
    if (days <= dailyCutoff) {
        barFirst.bars.barWidth = timeUnitSize.day;
        barSecond.bars.barWidth = timeUnitSize.day;
    } else if (days > dailyCutoff && days <= weeklyCutoff) {
        barFirst.bars.barWidth = timeUnitSize.week;
        barSecond.bars.barWidth = timeUnitSize.week;
    } else if (days > weeklyCutoff && days <= monthlyCutoff) {
        barFirst.bars.barWidth = timeUnitSize.month;
        barSecond.bars.barWidth = timeUnitSize.month;
    } else if (days > monthlyCutoff && days <= quarterlyCutoff) {
        barFirst.bars.barWidth = timeUnitSize.quarter;
        barSecond.bars.barWidth = timeUnitSize.quarter;
    } else if (days > quarterlyCutoff) {
        barFirst.bars.barWidth = timeUnitSize.year;
        barSecond.bars.barWidth = timeUnitSize.year;
    }
}

/* ---------------------------------------------------------------------------------------
 * Plot the data on the RT (Running Total) chart and initialize its "tooltip" popup function 
 * -------------------------------------------------------------------------------------*/
function PlotRT() {
    options.xaxis.show = true;
    $.plot($("#phRT"), dsRunningTotal, options);
    $("#phRT").UseTooltip();
}

/* ---------------------------------------------------------------------------------------
 * Plot the data on the CDND (Credits, Debits & Net Daily) chart and initialize its 
 * "tooltip" popup function. 
 * -------------------------------------------------------------------------------------*/
function PlotCDND() {
    InitCDNDDataset();
    options.xaxis.show = false;
    $.plot("#phCDND", dsCDND, options);
    $("#phCDND").UseTooltip();
}

/* ---------------------------------------------------------------------------------------
 * Initialize the "dsCDND" with child datasets (dsCredits, dsDebits & dsNet) based 
 * on the user's checkbox selections.
 * -------------------------------------------------------------------------------------*/
function InitCDNDDataset() {
    dsCDND = [[], [], []];
    choiceContainer.find("input:checked").each(function() {
        var key = parseInt($(this).attr("name"));
        switch (key) {
        case 0:
            dsCDND[key] = dsCredits;
            break;
        case 1:
            dsCDND[key] = dsDebits;
            break;
        case 2:
            dsCDND[key] = dsNet;
            break;
        }
    });
}

/* ---------------------------------------------------------------------------------------
 * Event function that displays a popup screen when user hovers over a chart data point.
 * It calls the "TooltipDialogPanel" function which constucts the panel within the screen.
 * -------------------------------------------------------------------------------------*/
$.fn.UseTooltip = function() {
    $(this).bind("plothover", function(event, pos, item) {
        TooltipDialogPanel(event, pos, item, MainList);
    });
};


/* Archived ********************************************************************************/
/* ---------------------------------------------------------------------------------------
 * This function takes the returned data and constructs the distinct list of items that
 * go into the PrimaryList dataset by using the "Date" field
 * -------------------------------------------------------------------------------------*/
function GetUniqueList(list) {
    var result = new Array();
    for (var i = 0; i < list.length; i++) {
        var exists = false;
        var x = list[i][0];
        for (var j = 0; j < result.length; j++) {
            var y = result[j][0];
            if (x === y) {
                exists = true;
            }
        }
        if (!exists)
            result.push(list[i]);
    }
    return result;
}
/******************************************************************************************/
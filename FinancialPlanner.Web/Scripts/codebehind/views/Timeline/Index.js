/* #######################################################################################
 * Javascript code-behind page for the Timeline view
 * ######################################################################################*/
/* ======================================================================================
 * Dataset and Chart Configurations
 * ======================================================================================*/
/* Declaration & Initialization */
var PrimaryList = new Array(),
    DetailList = new Array(),
    RunningTotalList = new Array(),
    CreditList = new Array(),
    DebitList = new Array(),
    NetDailyAmount = new Array(),
    buffer = 24 * 60 * 60 * 300,
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
        barWidth: buffer,
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
        barWidth: buffer,
        lineWidth: 1
    }
};
var dsNetDaily = {
    label: "Net Daily",
    data: NetDailyAmount,
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
/* Overall container for "dsCredits", "dsDebits" & "dsNetDaily" */
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
            tickFormatter: function (yvalue) {
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
        backgroundColor: { colors: ["#ffffff", "#EDF5FF"] }
    }
};

/* ======================================================================================
 * Document Complete
 * ======================================================================================*/
$(function () {
    choiceContainer = $("#choicesCDND");
    choiceContainer.find("input").click(PlotCDND);
});

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
function GetData() {
    var viewModel = {
        timeFrameBegin: $("#timeFrameBegin").val(),
        timeFrameEnd: $("#timeFrameEnd").val()
    };

    /* Diagnostic */
    viewModel.timeFrameBegin = "1/1/2016";
    viewModel.timeFrameEnd = "2/1/2016";

    /* Main ajax call to get the base data */
    $.ajax({
        type: "GET",
        url: "/Display/Timeline/GetLedgerReadout",
        data: viewModel,
        /* Take the return data and construct the datasets */
        success: function (dataList) {
            GetPrimaryList(dataList);
            GetDetailList(dataList);
        },
        /* Display any errors */
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.responseText.toString());
        },
        /* -----------------------------------------------------------------
         * Once the datasets are complete take the Primary List
         * and break it out into the data series that will be used
         * in the charts
         * ---------------------------------------------------------------*/
        complete: function () {
            RunningTotalList.length = 0;
            CreditList.length = 0;
            DebitList.length = 0;
            NetDailyAmount.length = 0;
            $.each(PrimaryList, function (index, value) {
                /* Running Total */
                var rt = [];
                rt.push(value[1], value[5]); // Date, Running Total
                RunningTotalList.push(rt);
                /* Credits */
                var c = [];
                c.push(value[1], value[2]); // Date, Credit Summary
                CreditList.push(c);
                /* Debits */
                var d = [];
                d.push(value[1], value[3]); // Date, Debit Summary
                DebitList.push(d);
                /* Net Daily Amounts */
                var nda = [];
                nda.push(value[1], value[4]); // Date, Net Daily Amount
                NetDailyAmount.push(nda);
            });
            PlotRT();
            PlotCDND();
        }
    });
}

/* ---------------------------------------------------------------------------------------
 * Creates a distinct list of the overall and summary fields which are repeated for a 
 * given date such as Date, Credit Summary, Debit Summary, Net Daily and RunningTotal. 
 * -------------------------------------------------------------------------------------*/
function GetPrimaryList(list) {
    PrimaryList.length = 0;
    $.each(list, function (index, value) {
        var result = new Array();
        result.push(
            value.PkLMain,
            GetTime(value.WDate),
            parseFloat(value.CreditSummary),
            parseFloat(value.DebitSummary),
            parseFloat(value.NetDaily),
            parseFloat(value.RunningTotal)
        );
        PrimaryList.push(result);
    });
    PrimaryList = GetUniqueList(PrimaryList);
}

/* ---------------------------------------------------------------------------------------
 * Creates a detail list of Credits and Debits items for each day
 * -------------------------------------------------------------------------------------*/
function GetDetailList(list) {
    DetailList.length = 0;
    $.each(list, function (index, value) {
        var result = new Array();
        result.push(
            value.FkItemDetail,
            GetTime(value.WDate),
            value.PkLMain,
            value.ItemType,
            value.PeriodName.toString(),
            value.Name.toString(),
            parseFloat(value.Amount)
        );
        DetailList.push(result);
    });
}

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
 * Initialize the "dsCDND" with child datasets (dsCredits, dsDebits & dsNetDaily) based 
 * on the user's checkbox selections.
 * -------------------------------------------------------------------------------------*/
function InitCDNDDataset() {
    dsCDND = [[], [], []];
    choiceContainer.find("input:checked").each(function () {
        var key = parseInt($(this).attr("name"));
        switch (key) {
            case 0:
                dsCDND[key] = dsCredits;
                break;
            case 1:
                dsCDND[key] = dsDebits;
                break;
            case 2:
                dsCDND[key] = dsNetDaily;
                break;
        }
    });
}

/* ---------------------------------------------------------------------------------------
 * Event function that displays a popup screen when user hovers over a chart data point.
 * It calls the "TooltipDialogPanel" function which constucts the panel within the screen.
 * -------------------------------------------------------------------------------------*/
$.fn.UseTooltip = function () {
    $(this).bind("plothover", function (event, pos, item) {
        TooltipDialogPanel(event, pos, item, DetailList);
    });
};
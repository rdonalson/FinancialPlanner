/* ======================================================================================
 * 
 * ======================================================================================*/
/* ---------------------------------------------------------------------------------------
 * 
 * -------------------------------------------------------------------------------------*/
function TooltipDialogPanel(event, pos, item, detailList) {
    if (item) {
        $("#tooltip").remove();

        var xvalue = item.datapoint[0];
        var yvalue = item.datapoint[1];
        var date = $.datepicker.formatDate("D mm/dd/yy", new Date(xvalue));

        var color = item.series.color;
        var itemFilter;
        switch (item.series.label) {
            case "Running Total":
                itemFilter = 0;     // No extra item detail
                break;
            case "Credits":
                itemFilter = 1;     // Credit detail
                break;
            case "Debits":
                itemFilter = 2;     // Debit detail     
                break;
            default:
                itemFilter = 0;
                break;
        }
        var sb = CreatePopupPanel(detailList, xvalue, yvalue, itemFilter, date, item.series.label);
        ShowTooltip(item.pageX, item.pageY, color, sb);
    } else {
        $("#tooltip").remove();
    }
}

/* ---------------------------------------------------------------------------------------
 * This function dynamically uses the items in the "detailList" to dynamically build the
 * structure in the "tooltip" popup.  
 * The data displayed consists of the basic Data Type, the X and Y values from the data point
 * and an itemized list that represents the data point.
 * -------------------------------------------------------------------------------------*/
function CreatePopupPanel(detailList, xvalue, yvalue, itemFilter, date, label) {
    var list = GetItemList(detailList, xvalue, itemFilter);
    var sb = "<table class='table table-condensed' >";
    sb = sb + "<thead>";
    sb = sb + "<tr class='row'>";
    sb = sb + "<th colspan='3' class='headerPopup'>" + label + "</th>";
    sb = sb + "</tr>";

    if (itemFilter !== 0) {
        sb = sb + "<tr class='row'>";
        sb = sb + "<th class='col-md-5' >" + date + "</th>";
        sb = sb + "<th class='col-md-5 alignRight' >Total:</th>";
        sb = sb + "<th class='col-md-7 alignRight'>" + FormatNumber(yvalue, 2, "$ ", ",", ".") + "</th>";
        sb = sb + "</tr>";

        sb = sb + "<tr class='row columnHeading'>";
        sb = sb + "<th >Period:</th>";
        sb = sb + "<th >Name:</th>";
        sb = sb + "<th class='alignRight'>Amount:</th>";
        sb = sb + "</tr>";
        sb = sb + "</thead>";

        sb = sb + "<tbody>";
        $.each(list, function (index, value) {
            sb = sb + "<tr class='row'>";
            sb = sb + "<td>" + value[4] + "</td>";
            sb = sb + "<td>" + value[5] + "</td>";
            sb = sb + "<td class='alignRight'>" + FormatNumber(value[6], 2, "$ ", ",", ".") + "</td>";
            sb = sb + "</tr>";
        });
        sb = sb + "</tbody>";
    } else {
        sb = sb + "<tr class='row'>";
        sb = sb + "<th class='col-md-5' >" + date + "</th>";
        sb = sb + "<th class='col-md-5 alignRight' >&nbsp;</th>";
        sb = sb + "<th class='col-md-7 alignRight'>" + FormatNumber(yvalue, 2, "$ ", ",", ".") + "</th>";
        sb = sb + "</tr>";
        sb = sb + "</thead>";
    }
    sb = sb + "</table>";
    return sb;
}

/* ---------------------------------------------------------------------------------------
 * 
 * -------------------------------------------------------------------------------------*/
function GetItemList(detailList, xvalue, itemFilter) {
    var result = [];
    $.each(detailList, function (index, value) {
        if (value[1] === xvalue && value[3] === itemFilter) {
            var row = new Array();
            row.push(
                value[0],
                value[1],
                value[2],
                value[3],
                value[4],
                value[5],
                value[6]
            );
            result.push(row);
        }
    });
    return result;
}

/* ---------------------------------------------------------------------------------------
 * 
 * -------------------------------------------------------------------------------------*/
function ShowTooltip(x, y, color, contents) {
    $("<div id=\"tooltip\" class=\"tooltipPopup\" >" + contents + "</div>").css({
        top: y - 40,
        left: x - 120,
        border: "2px solid " + color
    }).appendTo("body").fadeIn(200);
}

/* ---------------------------------------------------------------------------------------
 * Format 
 * -------------------------------------------------------------------------------------*/
function FormatNumber(number, places, symbol, thousand, decimal) {
    number = number || 0;
    places = !isNaN(places = Math.abs(places)) ? places : 2;
    symbol = symbol !== undefined ? symbol : "$";
    thousand = thousand || ",";
    decimal = decimal || ".";

    var leftP = "", rightP = "";
    if (number < 0) {
        leftP = "(", rightP = ")";
    }

    var i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "";
    var j = (j = i.length) > 3 ? j % 3 : 0;

    return symbol + leftP + (j ? i.substr(0, j) + thousand : "")
        + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand)
        + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "") + rightP;
}

/* ---------------------------------------------------------------------------------------
 * 
 * -------------------------------------------------------------------------------------*/
function GetTime(wdate) {
    if (typeof (wdate) !== "undefined") {
        var parsedDate = new Date(parseInt(wdate.substr(6)));
        var jsDate = new Date(parsedDate);
        return jsDate.getTime();
    }
    return "";
}
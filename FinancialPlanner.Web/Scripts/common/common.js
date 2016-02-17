/* ======================================================================================
 * 
 * ======================================================================================*/
/* ---------------------------------------------------------------------------------------
 * 
 * -------------------------------------------------------------------------------------*/
function TooltipDialogPanel(event, pos, item, mainList) {
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
        var sb = CreatePopupPanel(mainList, xvalue, yvalue, itemFilter, date, item.series.label);
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
function CreatePopupPanel(mainList, xvalue, yvalue, itemFilter, date, label) {
    var list = GetItemList(mainList, xvalue);
    var sb = "<table class='popuptable' >";
    sb = sb + "<thead>";
    sb = sb + "<tr class='titleheader'>";
    sb = sb + "<th colspan='4' >" + label + "</th>";
    sb = sb + "</tr>";

    if (itemFilter !== 0) {
        sb = sb + "<tr class='columnheaderWhite'>";
        sb = sb + "<th class='left100' colspan='2' >" + date + "</th>";
        sb = sb + "<th >Total:</th>";
        sb = sb + "<th class='right150'>" + FormatNumber(yvalue, 2, "$ ", ",", ".") + "</th>";
        sb = sb + "</tr>";

        sb = sb + "<tr class='columnheaderLightGray'>";
        sb = sb + "<th class='left100' >Occurance Date</th>";
        sb = sb + "<th >Period Type</th>";
        sb = sb + "<th >Item Name</th>";
        sb = sb + "<th class='right150'>Item Amount:</th>";
        sb = sb + "</tr>";
        sb = sb + "</thead>";

        sb = sb + "<tbody>";
        $.each(list.DetailsList, function (index, value) {
            if (value.ItemType === itemFilter) {
                sb = sb + "<tr >";
                sb = sb + "<td class='left100'>" + $.datepicker.formatDate("mm/dd/yy D", new Date(GetTime(value.OccurrenceDate))) + "</td>";
                sb = sb + "<td>" + value.PeriodName + "</td>";
                sb = sb + "<td>" + value.Name + "</td>";
                sb = sb + "<td class='right150'>" + FormatNumber(value.Amount, 2, "$ ", ",", ".") + "</td>";
                sb = sb + "</tr>";
            }
        });
        sb = sb + "</tbody>";
    } else {
        sb = sb + "<tr class='columnheaderWhite'>";
        sb = sb + "<th class='left100' colspan='2' >" + date + "</th>";
        sb = sb + "<th >Total:</th>";
        sb = sb + "<th class='right150'>" + FormatNumber(yvalue, 2, "$ ", ",", ".") + "</th>";
        sb = sb + "</tr>";
        sb = sb + "</thead>";
    }
    sb = sb + "</table>";
    return sb;
}

/* ---------------------------------------------------------------------------------------
 * 
 * -------------------------------------------------------------------------------------*/
function GetItemList(mainList, xvalue) {
    var result = [];
    $.each(mainList, function (index, value) {
        if (GetTime(value.WDate) === xvalue) {
            result = value;
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
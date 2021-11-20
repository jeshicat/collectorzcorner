$(document).ready(function () {
    $("#TypeList").prepend("<option selected='selected' disabled='disabled'>Select item type</option>");

    $("#TypeList").change(function () {
        $("#itemModel_TypeID").val($("#TypeList option:selected").val());
    });

    $("#SeriesList").change(function () {
        var selected = $("#SeriesList option:selected").val();
        $("#itemModel_SeriesID").val(selected);

        if (selected == "-1")
            $(".seriesName").show();
        else
            $(".seriesName").hide();
    });

    $("#itemModel_ReleaseDate").datepicker({
        changeYear: true,
        changeMonth: true,
        dateFormat: 'M dd, yy'
    });

    $("#NextBtn").click(function () {
        var typeID = $("#itemModel_TypeID").val();
        if (typeID != 0) {
            PopulateSeriesList(typeID);
            $("#ErrorMsg").hide();
            $("#Step1").hide();
            $("#Step2").show();
            $("#" + typeID).show();
            $("#BackBtn").show();
            $(this).hide();
            //            $.post("/Search/SendItemPartial", { TypeID: typeID }, function (result) {
            //                $("#DynamicContent").html(result);
            //            });

            $.ajax({
                url: "/Search/PopulateGenres",
                data: { TypeID: typeID },
                success: function (result) {
                    var count = 0;
                    var genres = $.parseJSON(result);

                    $('#GenreGroup').html('<table><tr>');
                    for (var g in genres) {
                        if (genres.hasOwnProperty(g)) {
                            if (count < 3) {
                                count++;
                                $('#GenreGroup').append('<td style="padding-right:10px;"><label class="checkbox"><input type="checkbox" name="' + genres[g] + '" value="' + g + '">' + genres[g] + '</td></label>');
                            }
                            else {
                                count = 1;
                                $('#GenreGroup').append('</tr><tr>');
                                $('#GenreGroup').append('<td style="padding-right:10px;"><label class="checkbox"><input type="checkbox" name="' + genres[g] + '" value="' + g + '">' + genres[g] + '</td></label>');
                            }
                        }
                    }
                    $('#GenreGroup').append("</tr></table>");

                    if (typeID == "MOV" || typeID == "GAM") {
                        $("#publisher").text("Studio:");
                    }
                    else {
                        $("#publisher").text("Publisher:");
                    }
                }
            });

            $.ajax({
                url: "/Search/SendItemPartial",
                data: { TypeID: typeID },
                success: function (result) {
                    $("#DynamicContent").html(result);
                    PopulateRatingList(typeID);
                    PopulatePlatformList();

                    $("#MovieRatingList").change(function () {
                        $("#movieModel_ContentRatingID").val($("#MovieRatingList option:selected").val());
                    });

                    $("#GameRatingList").change(function () {
                        $("#gameModel_ContentRatingID").val($("#GameRatingList option:selected").val());
                    });

                    $("#PlatformList").change(function () {
                        $("#gameModel_PlatformID").val($("#PlatformList option:selected").val());
                    });
                },
                complete: function () {
                    $("form").each(function () { $.data($(this)[0], 'validator', false); });
                    $.validator.unobtrusive.parse("form");
                }
            });

        }
        else
            $("#ErrorMsg").show();
    });

    $("#BackBtn").click(function () {
        var typeID = $("#itemModel_TypeID").val();
        $("#Step1").show();
        $("#Step2").hide();
        $("#" + typeID).hide();
        $("#NextBtn").show();
        $(this).hide();
    });
});

function PopulateSeriesList(typeID) {
    $.ajax({
        url: "/Search/PopulateSeriesList",
        data: { typeID: typeID },
        success: function (result) {
            $('#SeriesList').html('<option selected="selected" disabled="disabled">Choose series</option>');
            var series = $.parseJSON(result);
            $.each(series, function () {
                $('#SeriesList').append('<option value=' + this.id + '>' + this.name + '</option>');
            });

            $('#SeriesList').append('<option value="-1">Other</option>');
        }
    });
}

function PopulatePlatformList() {
    $.ajax({
        url: "/Search/PopulatePlatformList",
        success: function (result) {
            $('#PlatformList').html('<option selected="selected" disabled="disabled">Choose platform</option>');
            var platform = $.parseJSON(result);
            for (var p in platform) {
                if (platform.hasOwnProperty(p)) {
                    $('#PlatformList').append('<option value=' + p + '>' + platform[p] + '</option>');
                }
            }

            //$('#PlatformList').append('<option value="-1">Other</option>');
        }
    });
}

function PopulateRatingList(typeID) {
    $.ajax({
        url: "/Search/PopulateRatingList",
        data: { typeID: typeID },
        success: function (result) {
            $('.ratingList').html('<option selected="selected" disabled="disabled">Choose rating</option>');
            var rating = $.parseJSON(result);
            for (var r in rating) {
                if (rating.hasOwnProperty(r)) {
                    $('.ratingList').append('<option value=' + r + '>' + rating[r] + '</option>');
                }
            }                    
        }
    });
}

function ConstructGenreJSONString(form) {
    var genre = null;
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == 'checkbox') {
            if (form.elements[i].checked) {
                if (genre != null)
                    genre += ";" + form.elements[i].value;
                else
                    genre = form.elements[i].value;
            }
        }
    }
    $('#itemModel_Genres').val(genre); 
}

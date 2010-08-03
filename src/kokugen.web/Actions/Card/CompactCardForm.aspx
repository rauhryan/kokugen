<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.CompactCardForm"%>
<%@ Import Namespace="Kokugen.Web.Actions.Card"%>
<%@ Import Namespace="HtmlTags"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>

<div id="compact-card-container" class="hidden">
<div class="error hidden"><span></span></div>
<%= this.FormFor(new CompactCardFormModel()).Id("card-form-compact")%>
    <div class="add-card-compact">
        <div class="full-width-input">
            <%= this.InputFor(x => x.Card.Title).Hint("Enter description of card") %>
        </div>
        <div class="quarter-input">
            <%= this.InputFor(x => x.Card.Size).Id("card-size-box").Hint("Card Size (Optional)") %>
        </div>
        <div class="quarter-input">
            <%= this.InputFor(x => x.Card.Priority).Hint("Priority") %>
        </div>
        <div class="quarter-input">
            <%= this.InputFor(x => x.Card.Deadline).Hint("Deadline") %>
        </div>
        <div class="quarter-input">
            <%= this.InputFor(x => x.UserId).Hint("Who is Assigned to this") %>
        </div>
        <%= this.InputFor(x => x.Card.Id).Hide() %>
        <%= this.InputFor(x => x.ProjectId).Hide() %>
        <%= this.InputFor(x => x.Card.Details).Id("card-details").Hide() %>
        <input type="hidden" name="SubmitType" id="submitType" />
        <div class="actions">
             <button id="details-button" class="btn">Details</button>
             <input type="submit" name="SubmitButton" value="Hang on Board" id="hang-button" class="button grn" />
             <input type="submit" name="SubmitButton" value="Add to Backlog" id="save-button" class="button grn"/>
             
         </div>
     </div>
     <div class="bottom-border"></div>
     <div class="clear"></div>
<%= this.EndForm() %>

<div class="hidden" id="details-editor">
<textarea rows="15" cols="69" id="details-text"></textarea>
</div>

</div>

<script type="text/javascript">

    function updateBoard(data) {
        var card = new Card(data.Item);
        _cards.push(card);
        $("#backlog-container ul.card-list").append(buildCardDisplay(card));

        $("#compact-card-container").slideToggle('medium');
        $("form").hintify();
    }

    function hangOnBoard(data) {
        var card = new Card(data.Item);
        _cards.push(card);
        var col = $(".column ul.card-list")[1];
        var card = buildCardDisplay(card);
        $(col).append(card);

        $("#compact-card-container").slideToggle('medium');
        $("form").hintify();
    }

    $(document).ready(function () {

        $("#details-editor").dialog({ modal: true, resizable: false, title: "Card Details", autoOpen: false, width: 700, height: 500, closeOnEscape: true, buttons: { "OK": function () {

            var data = $("#details-text").val();
            $("#card-details").val(data);
            $(this).dialog("close");
        }, "Cancel": function () { $(this).dialog("close"); }
        }
        });



        $("#details-button").click(function () { $("#details-editor").dialog('open'); return false; });
        $("#card-form-compact").validate({ errorClass: "error", ignoreTitle: true,

            invalidHandler: function (e, validator) {
                var errors = validator.numberOfInvalids();
                if (errors) {
                    var message = errors == 1 ? 'You missed 1 field. It has been highlighted below' : 'You missed ' + errors + ' fields. They have been highlighted below';
                    $("div.error span").html(message);
                    $("div.error").show();
                } else {
                    $("div.error").hide();
                }
            }
        });
        $('#save-button').submit(function () {
            //            if ($('#card-size-box').val() == "Card Size (Optional)")
            //            { $('#card-size-box').val(''); }
            $("[_hint]").each(function () { $(this).removeHint() });
            
            ValidateAndSave(updateBoard, $("#card-form-compact"));
            return false;
        });

        $('#hang-button').submit(function () {
            $("[_hint]").each(function () { $(this).removeHint() });
            $('#submitType').val("Hang on Board");
            ValidateAndSave(hangOnBoard, $("#card-form-compact"));
            return false;
        });

    });
</script>
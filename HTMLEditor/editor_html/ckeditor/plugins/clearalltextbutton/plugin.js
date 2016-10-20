(function () {
    //Section 1 : Code to execute when the toolbar button is pressed
    var a = {
        exec: function (editor) {
            window.location.reload(true);
        }
    },

        //Section 2 : Create the button and add the functionality to it
        b = 'clearalltextbutton';
    CKEDITOR.plugins.add(b, {
        init: function (editor) {
            editor.addCommand(b, a);
            editor.ui.addButton("clearalltextbutton", {
                label: 'Clear Text',
                icon: this.path.replace("(", "%28").replace(")", "%29") + "clear-icon2.png",
                command: b
            });
        }
    });
})();
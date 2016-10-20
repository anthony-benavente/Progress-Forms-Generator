var colorpickerself = undefined;
CKEDITOR.plugins.add( 'colorpicker', {
    onLoad: function() {
        console.log(this);
    },
    init: function(editor) {
        colorpickerself = this;
		editor.ui.addButton( 'colorpicker', {
			label: 'Color Picker',
			icon: this.path + '../simplebutton/images/simplebutton.png'
		});
    }
});
/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {

	// %REMOVE_START%
	// The configuration options below are needed when running CKEditor from source files.
	config.plugins = 'colordialog,dialogui,dialog,about,a11yhelp,basicstyles,blockquote,clipboard,panel,' +
		'floatpanel,menu,resize,button,toolbar,elementspath,enterkey,entities,' +
		'popup,filebrowser,floatingspace,listblock,richcombo,format,horizontalrule,htmlwriter,' +
		'wysiwygarea,image,indent,indentlist,fakeobjects,link,list,magicline,maximize,pastetext,' +
		'pastefromword,removeformat,showborders,sourcearea,specialchar,menubutton,scayt,' +
		'showblocks,stylescombo,tab,table,tabletools,undo,find,font,justify';

	config.extraPlugins = 'colorbutton,wsc,indentblock,uicolor,clearalltextbutton,simplebutton';

	config.skin = 'kama';
	// %REMOVE_END%

	// Define changes to default configuration here.
	// For complete reference see:
	// http://docs.ckeditor.com/#!/api/CKEDITOR.config

	// The toolbar groups arrangement, optimized for two toolbar rows.
	config.toolbarGroups = [
		{name: 'everything',    groups: [ 'mode', 'colors', 'document', 'doctools', 'clipboard', 'find', 'selection', 'spellchecker', 'links', 'insert', 'forms', 'basicstyles', 'list']}
		,'/',
		{name: 'styleStuff',    groups: [ 'tools', 'paragraph', 'indent', 'align', 'bidi', 'styles', 'others']}
    ];
	// config.toolbarGroups = [
	// 	{name: 'items', gropus: ['mode']}]

	// Remove some buttons provided by the standard plugins, which are
	// not needed in the Standard(s
	config.removeButtons = 'Strike,Subscript,Superscript,Maximize,UIColor';

	// Allows the creation of a full html page
	config.fullPage = false;

	// Set the most common block elements.
	config.format_tags = 'p;h1;h2;h3;pre';
	config.extraAllowedContent = {
		"font": { styles: '*', attributes: '*' },
		"table": { styles: '*', attributes: '*' },
		"center": { styles: '*', attributes: '*' },
		"p": { attributes: '*', classes: '*', styles:'*' },
		"style": { attributes: '*'},
		"td": { attributes: "*", styles: "*" },
		"h3": {  styles: '*', attributes: '*' },
		"a": {  styles: '*', attributes: '*'  }
	};

	config.left = 0;
	config.width = '100%';
	var toolheight = 75;

	if (window.innerHeight !== undefined) {
		config.height = (window.innerHeight - toolheight).toString();
	} else {
		var bod = document.body, doc = document.documentElement;
		config.height = (Math.max(bod.clientHeight, doc.clientHeight) - toolheight).toString();
	}

	// Simplify the dialog windows.
	config.removeDialogTabs = 'image:advanced;link:advanced';

	//Remove HTML tags in footer.
	config.removePlugins = 'elementspath';
	config.resize_enabled = false;

	// Fix paste from word removing styles
	config.pasteFromWordRemoveStyles = false;
	config.pasteFromWordRemoveFontStyles = false;

	config.entities_latin = false;
	config.entities_additional = '';

	config.font_names =
            'Arial/Arial, Helvetica, sans-serif;' +
            'Comic Sans MS/Comic Sans MS, cursive;' +
            'Courier New/Courier New, Courier, monospace;' +
            'Georgia/Georgia, serif;' +
            'Helvetica/Helvetica, Arial, sans-serif;' +
            'Lucida Sans Unicode/Lucida Sans Unicode, Lucida Grande, sans-serif;' +
            'Tahoma/Tahoma, Geneva, sans-serif;' +
            'Times New Roman/Times New Roman, Times, serif;' +
            'Trebuchet MS/Trebuchet MS, Helvetica, sans-serif;' +
            'Calibri/Calibri, Verdana, Geneva, sans-serif;' + /* here is your font */
            'Verdana/Verdana, Geneva, sans-serif';

    config.sourceAreaTabSize = 2;

	config.allowedContent = true;

};

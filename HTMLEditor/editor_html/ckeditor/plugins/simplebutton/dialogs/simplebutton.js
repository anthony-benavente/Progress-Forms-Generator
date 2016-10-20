/*

	This file is a part of simplebuttion project.

	Copyright (C) Thanh D. Dang <thanhdd.it@gmail.com>

	simplebuttion is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	simplebuttion is distributed in the hope that it will be useful, but
	WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
	General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/
var carlosButton = '<div class="table-responsive" align="center"><table border="0" cellpadding="0" cellspacing="0" class="table" style="cursor:pointer;cursor:hand;"><tbody> <tr> <td align="center" bgcolor="{{backgroundColor}}" style="border: {{border}}; padding: 12px 18px; border-radius: {{borderRadius}}; -webkit-border-radius: {{borderRadius}};"><a href="{{href}}" rel="nofollow" style="text-decoration:none;color:{{color}};font-size:{{fontSize}};font-weight: normal;" target="_blank">{{text}}</a></td> </tr></tbody></table></div></div>';
		
CKEDITOR.dialog.add('simplebuttonDialog', function(editor) {
	var generateDefaultTable = function() {
		var table = editor.document.createElement('table');
		table.setAttribute('border', '0');
		table.setAttribute('cellpadding', '0');
		table.setAttribute('cellspacing', '0');
		table.setAttribute('align', 'center');
		table.addClass('simple-button-plugin');
		table.setAttribute('style', 'cursor:pointer;cursor:hand;');

		var tbody = editor.document.createElement('tbody');
		var tr = editor.document.createElement('tr');
		var td = editor.document.createElement('td');
		td.setAttribute('align', 'center');
		td.setAttribute('bgcolor', '#ffffff');
		td.setAttribute('style', 'border-width: 1px; border-type: solid; border-style: solid; border-color: lightgray; padding: 12px 18px; border-radius: 0px; -webkit-border-radius: 0px;');

		var a = editor.document.createElement('a');
		a.setAttribute('href', '');
		a.setAttribute('rel', 'nofollow');
		a.setAttribute('style', 'text-decoration:none;color:#000000;font-size:12px;font-weight: normal;');
		a.setAttribute('target', '_blank');
		a.setHtml('Example Text');

		td.append(a);
		tr.append(td);
		tbody.append(tr);
		table.append(tbody);
		return table;
	}
	
	var generateCarlosButton = function(options) {
		var defaultOptions = {
			text: '',
			color: '#000',
			backgroundColor: '#fff',
			href:'',
			borderSize: '1px',
			borderColor: '#fff',
			borderRadius: '3px'
		};
		CKEDITOR.tools.extend(options, defaultOptions);
		options.border = options.borderSize + ' solid ' + options.borderColor;

		var toInsert = carlosButton;
		for (var key in options) {
			toInsert = toInsert.replace(new RegExp("{{" + key + "}}", "g"), options[key]);
		}
		return toInsert;
	}
	var data = {};

	var result = {
		title: 'Simple Button',
		minWidth: 400,
		minHeight: 200,
		contents: [
			{
				id: 'tab-basic',
				elements: [
					{
						id: 'button-text',
						type: 'text',
						label: 'Text',
						commit: function(element) {
							 element.findOne('a').setText(this.getValue());
						},
						setup: function(element) {
							this.setValue(element.findOne('a').getText());
						}
					},
					{
						type: 'vbox',
						id: 'urlOptions',
						children: [
							{
							type: 'hbox',
							widths: [ '25%', '75%' ],
							children: [
								{
								id: 'protocol',
								type: 'select',
								label: 'Protocol',
								'default': 'http://',
								items: [
									// Force 'ltr' for protocol names in BIDI. (#5433)
									[ 'http://\u200E', 'http://' ],
									[ 'https://\u200E', 'https://' ],
									[ 'ftp://\u200E', 'ftp://' ],
									[ 'news://\u200E', 'news://' ]
									],
								setup: function( element, data ) {
									if ( data.url )
										this.setValue( data.url.protocol || '' );
								},
								commit: function( element, data ) {
									if ( !data.url )
										data.url = {};

									data.url.protocol = this.getValue();
								}
							},
								{
								type: 'text',
								id: 'url',
								label: 'URL',
								required: true,
								onLoad: function() {
									this.allowOnChange = true;
								},
								onKeyUp: function() {
									this.allowOnChange = false;
									var protocolCmb = this.getDialog().getContentElement( 'info', 'protocol' ),
										url = this.getValue(),
										urlOnChangeProtocol = /^(http|https|ftp|news):\/\/(?=.)/i,
										urlOnChangeTestOther = /^((javascript:)|[#\/\.\?])/i;

									var protocol = urlOnChangeProtocol.exec( url );
									if ( protocol ) {
										this.setValue( url.substr( protocol[ 0 ].length ) );
										protocolCmb.setValue( protocol[ 0 ].toLowerCase() );
									} else if ( urlOnChangeTestOther.test( url ) )
										protocolCmb.setValue( '' );

									this.allowOnChange = true;
								},
								onChange: function() {
									if ( this.allowOnChange ) // Dont't call on dialog load.
									this.onKeyUp();
								},
								validate: function() {
									var dialog = this.getDialog();

									if ( dialog.getContentElement( 'info', 'linkType' ) && dialog.getValueOf( 'info', 'linkType' ) != 'url' )
										return true;

									if ( !editor.config.linkJavaScriptLinksAllowed && ( /javascript\:/ ).test( this.getValue() ) ) {
										alert( 'Invalid value!' );
										return false;
									}

									if ( this.getDialog().fakeObj ) // Edit Anchor.
									return true;

									var func = CKEDITOR.dialog.validate.notEmpty( 'Please type the link to the URL' );
									return func.apply( this );
								},
								setup: function( element, data ) {
									this.allowOnChange = false;
									if ( data.url )
										this.setValue( data.url.url );
									this.allowOnChange = true;

								},
								commit: function( element, data ) {
									// IE will not trigger the onChange event if the mouse has been used
									// to carry all the operations #4724
									this.onChange();

									if ( !data.url )
										data.url = {};

									data.url.url = this.getValue();
									this.allowOnChange = false;
								}
							}
							],
							setup: function( element, data ) {
								if ( !this.getDialog().getContentElement( 'info', 'linkType' ) )
									this.getElement().show();
							}
						}
						]
					},
					{
						type: 'hbox',
						widths: ['25%', '25%', '25%', '25%'],
						children: [
							{
								id: 'button-text-size',
								type: 'text',
								label: 'Font Size',
								'default': '12pt',
								commit: function(element) {
									element.findOne('a').setStyle('font-size', this.getValue());
								},
								setup: function(element) {
									this.setValue(element.findOne('a').getStyle('font-size') || '12pt');
								}
							},
							{
								id: 'button-align',
								type: 'select',
								label: 'Alignment',
								items: [
									[ 'left' ], ['center'], ['right']
								],
								'default': 'left',
								setup: function(element) {
									this.setValue(element.getAttribute('align') || 'left');
								},
								commit: function(element) {
									element.setAttribute('align', this.getValue());
								}
							},
							{
								id: 'button-border',
								type: 'text',
								label: 'Border Size',
								'default': '0',
								commit: function(element) {
									element.findOne('td').setStyle('border-width', this.getValue());
								},
								setup: function(element) {
									this.setValue(element.findOne('td').getStyle('border-width'));
								}
							},
							{
								id: 'button-border-radius',
								type: 'text',
								label: 'Border Radius',
								'default': '3px',
								commit: function(element) {
									element.findOne('td').setStyle('border-radius', this.getValue());
								},
								setup: function(element) {
									this.setValue(element.findOne('td').getStyle('border-radius'));
								}
							}
						]
					},
					{
						type: 'hbox',	
						widths: ['33%', '33%', '33%'],
						children: [
							{
								type: 'html',
								html: '<p>Border Color</p><input id="bordercolor-text-input" class="jscolor" value="#ffffff">'
							},
							{
								type: 'html',
								html: '<p>Background Color</p><input id="backcolor-text-input" class="jscolor" value="#ffffff">'
							},
							{
								type: 'html',
								html: '<p>Text Color</p><input id="color-text-input" class="jscolor" value="#000000">'
							}
						]
					},
					{
						// This element's purpose is just to initialize javascript for the previous elements'
						type : 'html',
						html: '<span style="display:none;"></span>',
						setup: function(element) {
							var td = element.findOne('td');
							var a = element.findOne('a');

							$('#backcolor-text-input').val(td.getAttribute('bgcolor'));
							$('#color-text-input').val(a.getStyle('color'));
							$('#bordercolor-text-input').val(td.getStyle('border-color'));
							// $('#align-options').val(element.getAttribute('align'));
						}	
					}
				]
			}
		],

		onShow: function() {
			var selection = editor.getSelection();
			var element = selection.getStartElement();

			if (element) {
				element = element.getAscendant({ table: 1 });
			}

			if (!element || !element.hasClass('simple-button-plugin')) {
				this.element = generateDefaultTable();
				this.insertMode = true;

				$('#backcolor-text-input,#color-text-input,#bordercolor-text-input').spectrum({
					preferredFormat: "hex",
					showInput: true,
					allowEmpty: true
				});
			} else {
				this.insertMode = false;
				
				if (!this.data) {
					this.data = { url: {} };
				}

				var a = element.findOne('a');
				var td = element.findOne('td');

				this.data.url.protocol = a.getAttribute('href').split('://')[0] + '://';
				this.data.url.url = a.getAttribute('href').split('://')[1];
				this.setupContent(element, this.data);

				$('#backcolor-text-input').spectrum('set', td.getAttribute('bgcolor'));
				$('#color-text-input').spectrum('set', a.getStyle('color'));
				$('#bordercolor-text-input').spectrum('set', td.getStyle('border-color'));
			}

			var document = this.getElement().getDocument();
			var preview_button = document.getById( 'preview-button' );
		},

		onOk: function() {	
			this.data = {};
			this.commitContent(this.element, this.data);

			var td = this.element.findOne('td');
			var a = this.element.findOne('a');

			td.setStyle('border-color', $('#bordercolor-text-input').val());
			td.setAttribute('bgcolor', $('#backcolor-text-input').val());
			a.setStyle('color', $('#color-text-input').val());

			a.setAttribute('href', this.data.url.protocol + this.data.url.url);

			if (this.insertMode) {
				editor.insertElement(this.element);
			}
		}
	};

	return result;
});


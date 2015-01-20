//
// Copyright (c) 2014-2015 Håkan Edling
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
// 
// http://github.com/piranhacms/piranha.vnext
// 

if (!manager.models)
	manager.models = {};

//
// Block view model
//
manager.models.block = function (locale) {
	'use strict';

	var self = window.block = this;

	// Labels & texts
	self.addTitle = locale.addTitle;
	self.editTitle = locale.editTitle;

	// Members
	self.title = ko.observable(self.addTitle);
	self.id = ko.observable('');
	self.name = ko.observable('');
	self.nameValid = ko.observable(true);
	self.slug = ko.observable('');
	self.description = ko.observable('');
	self.descriptionValid = ko.observable(true);
	self.body = ko.observable('');
	self.items = ko.observableArray([]);

	// Initializes the model
	self.init = function () {
		$.ajax({
			url: baseUrl + 'manager/blocks/get',
			type: 'GET',
			dataType: 'json',
			success: function (result) {
				if (result.success)
					self.items(result.data);
			},
			error: function (result) {
				console.log('error');
			}
		});
	};

	// Edits the specified block
	self.edit = function (id) {
		$.ajax({
			url: baseUrl + 'manager/block/get/' + id,
			type: 'GET',
			dataType: 'json',
			success: function (result) {
				if (result.success) {
					self.title(self.editTitle);
					self.id(result.data.Id);
					self.name(result.data.Name);
					self.nameValid(true);
					self.slug(result.data.Slug);
					self.description(result.data.Description);
					self.descriptionValid(true);
					self.body(result.data.Body);
					tinyMCE.activeEditor.setContent(result.data.Body != null ? result.data.Body : '');
					$('.collapse').collapse('show');
				}
				$('.table tr').removeClass('active');
				$('.table tr[data-id="' + id + '"]').addClass('active');
			},
			error: function (result) {
				console.log('error');
			}
		});
	};

	// Validates the current model
	self.validate = function () {
		var ret = true;

		if (self.name() == null || self.name() == '' || self.name().length > 128) {
			self.nameValid(false);
			ret = false;
		}
		if (self.description() != null && self.description().length > 255) {
			self.descriptionValid(false);
			ret = false;
		}
		return ret;
	};

	// Saves the current model
	self.save = function () {
		if (self.validate()) {
			self.body(tinyMCE.activeEditor.getContent());

			$.ajax({
				url: baseUrl + 'manager/block/save',
				type: 'POST',
				dataType: 'json',
				contentType: 'application/json',
				data: JSON.stringify({
					Id: self.id(),
					Name: self.name(),
					Slug: self.slug(),
					Description: self.description(),
					Body: self.body()
				}),
				success: function (result) {
					if (result.success) {
						self.items(result.data);

						// Handle list save fx
						setTimeout(function () {
							$('tr.pre-saved').removeClass('pre-saved').addClass('saved');
							setTimeout(function () {
								$('tr.saved').removeClass('saved').addClass('post-saved');
							}, 500)
						}, 200);

						// Handle panel save fx
						manager.notifySave($('.panel'));
					}
				},
				error: function (result) {
					console.log('error');
				}
			});
		}
	};

	// Deletes the specified block
	self.delete = function (id) {
		$.ajax({
			url: baseUrl + 'manager/block/delete/' + id,
			type: 'GET',
			contentType: 'application/json',
			success: function (result) {
				if (result.success)
					self.items(result.data);
			},
			error: function (result) {
				console.log('error');
			}
		});
	};

	// Clears the model and collapses the form
	self.clear = function () {
		self.title(self.addTitle);
		self.id('');
		self.name('');
		self.nameValid(true);
		self.slug('');
		self.description('');
		self.descriptionValid(true);
		self.body('');
		tinyMCE.activeEditor.setContent('');
		$('.collapse').collapse('hide');
		$('.table tr').removeClass('active');
	}

	// Initialze after everything is created.
	self.init();
};
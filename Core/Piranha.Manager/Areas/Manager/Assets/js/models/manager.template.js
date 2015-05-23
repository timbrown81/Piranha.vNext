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
// Template view model
//
manager.models.template = function (locale) {
	'use strict';

	var self = window.template = this;

	// Labels & texts
	self.addTitle = locale.addTitle;
	self.editTitle = locale.editTitle;

	// Members
	self.panelTitle = ko.observable(self.addTitle);
	self.id = ko.observable('');
	self.type = ko.observable('');
	self.typeValid = ko.observable(true);
	self.name = ko.observable('');
	self.nameValid = ko.observable(true);
	self.route = ko.observable('');
	self.view = ko.observable('');
	self.items = ko.observableArray([]);

	// Initializes the model
	self.init = function () {
		$.ajax({
			url: baseUrl + 'manager/templates/get',
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

	// Edits the specified author
	self.edit = function (id) {
		$.ajax({
			url: baseUrl + 'manager/category/get/' + id,
			type: 'GET',
			dataType: 'json',
			success: function (result) {
				if (result.success) {
					self.panelTitle(self.editTitle);
					self.id(result.data.Id);
					self.title(result.data.Title);
					self.titleValid(true);
					self.slug(result.data.Slug);
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

		if (self.type() == null || self.type() == '') {
			self.typeValid(false);
			ret = false;
		}

		if (self.name() == null || self.name() == '' || self.name().length > 128) {
			self.nameValid(false);
			ret = false;
		}
		return ret;
	};

	// Saves the current model
	self.save = function () {
		if (self.validate()) {
			$.ajax({
				url: baseUrl + 'manager/template/save',
				type: 'POST',
				dataType: 'json',
				contentType: 'application/json',
				data: JSON.stringify({
					Id: self.id(),
					Type: self.type(),
					Name: self.name(),
					Route: self.route(),
					View: self.view()
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

	// Deletes the specified template
	self.delete = function (id) {
		$.ajax({
			url: baseUrl + 'manager/template/delete/' + id,
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
		self.panelTitle(self.addTitle);
		self.id('');
		self.type('');
		self.typeValid(true);
		self.name('');
		self.nameValid(true);
		self.route('');
		self.view('');
		$('.collapse').collapse('hide');
		$('.table tr').removeClass('active');
	}

	// Initialze after everything is created.
	self.init();
};
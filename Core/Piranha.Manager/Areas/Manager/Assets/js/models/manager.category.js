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
// Category view model
//
manager.models.category = function (locale) {
	'use strict';

	var self = window.category = this;

	// Labels & texts
	self.addTitle = locale.addTitle;
	self.editTitle = locale.editTitle;

	// Members
	self.panelTitle = ko.observable(self.addTitle);
	self.id = ko.observable('');
	self.title = ko.observable('');
	self.titleValid = ko.observable(true);
	self.slug = ko.observable('');
	self.items = ko.observableArray([]);

	// Initializes the model
	self.init = function () {
		$.ajax({
			url: baseUrl + 'manager/categories/get',
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

		if (self.title() == null || self.title() == '' || self.title().length > 128) {
			self.titleValid(false);
			ret = false;
		}
		return ret;
	};

	// Saves the current model
	self.save = function () {
		if (self.validate()) {
			$.ajax({
				url: baseUrl + 'manager/category/save',
				type: 'POST',
				dataType: 'json',
				contentType: 'application/json',
				data: JSON.stringify({
					Id: self.id(),
					Title: self.title(),
					Slug: self.slug()
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

	// Deletes the specified author
	self.delete = function (id) {
		$.ajax({
			url: baseUrl + 'manager/category/delete/' + id,
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
		self.title('');
		self.titleValid(true);
		self.slug('');
		$('.collapse').collapse('hide');
		$('.table tr').removeClass('active');
	}

	// Initialze after everything is created.
	self.init();
};
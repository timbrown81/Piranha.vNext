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
// Alias view model
//
manager.models.alias = function (locale) {
	'use strict';

	var self = window.alias = this;

	// Labels & texts
	self.addTitle = locale.addTitle;
	self.editTitle = locale.editTitle;

	// Members
	self.title = ko.observable(self.addTitle);
	self.id = ko.observable('');
	self.oldUrl = ko.observable('');
	self.oldUrlValid = ko.observable(true);
	self.newUrl = ko.observable('');
	self.newUrlValid = ko.observable(true);
	self.isPermanent = ko.observable(false);
	self.items = ko.observableArray([]);

	// Initializes the model
	self.init = function () {
		$.ajax({
			url: baseUrl + 'manager/aliases/get',
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
			url: baseUrl + 'manager/alias/get/' + id,
			type: 'GET',
			dataType: 'json',
			success: function (result) {
				if (result.success) {
					self.title(self.editTitle);
					self.id(result.data.Id);
					self.oldUrl(result.data.OldUrl);
					self.oldUrlValid(true);
					self.newUrl(result.data.NewUrl);
					self.newUrlValid(true);
					self.isPermanent(result.data.IsPermanent);
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

		if (self.oldUrl() == null || self.oldUrl() == '' || self.oldUrl().length > 255) {
			self.oldUrlValid(false);
			ret = false;
		}
		if (self.newUrl() == null || self.newUrl() == '' || self.newUrl().length > 255) {
			self.newUrlValid(false);
			ret = false;
		}
		return ret;
	};

	// Saves the current model
	self.save = function () {
		if (self.validate()) {
			$.ajax({
				url: baseUrl + 'manager/alias/save',
				type: 'POST',
				dataType: 'json',
				contentType: 'application/json',
				data: JSON.stringify({
					Id: self.id(),
					OldUrl: self.oldUrl(),
					NewUrl: self.newUrl(),
					IsPermanent: self.isPermanent()
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
			url: baseUrl + 'manager/alias/delete/' + id,
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
		self.oldUrl('');
		self.oldUrlValid(true);
		self.newUrl('');
		self.newUrlValid(true);
		self.isPermanent(false);
		$('.collapse').collapse('hide');
		$('.table tr').removeClass('active');
	}

	// Initialze after everything is created.
	self.init();
};
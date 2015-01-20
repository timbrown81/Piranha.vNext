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
// Author view model
//
manager.models.author = function (locale) {
	'use strict';

	var self = window.author = this;

	// Labels & texts
	self.addTitle = locale.addTitle;
	self.editTitle = locale.editTitle;

	// Members
	self.title = ko.observable(self.addTitle);
	self.id = ko.observable('');
	self.name = ko.observable('');
	self.nameValid = ko.observable(true);
	self.email = ko.observable('');
	self.emailValid = ko.observable(true);
	self.description = ko.observable('');
	self.gravatarUrl = ko.observable('');
	self.items = ko.observableArray([]);

	// Initializes the model
	self.init = function () {
		$.ajax({
			url: baseUrl + 'manager/authors/get',
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
			url: baseUrl + 'manager/author/get/' + id,
			type: 'GET',
			dataType: 'json',
			success: function (result) {
				if (result.success) {
					self.title(self.editTitle);
					self.id(result.data.Id);
					self.name(result.data.Name);
					self.nameValid(true);
					self.email(result.data.Email);
					self.emailValid(true);
					self.description(result.data.Description);
					self.gravatarUrl(result.data.GravatarUrl);
					tinyMCE.activeEditor.setContent(result.data.Description != null ? result.data.Description : '');
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
		if (self.email() != null && self.email().length > 128) {
			self.emailValid(false);
			ret = false;
		}
		return ret;
	};

	// Saves the current model
	self.save = function () {
		if (self.validate()) {
			self.description(tinyMCE.activeEditor.getContent());

			$.ajax({
				url: baseUrl + 'manager/author/save',
				type: 'POST',
				dataType: 'json',
				contentType: 'application/json',
				data: JSON.stringify({
					Id: self.id(),
					Name: self.name(),
					Email: self.email(),
					Description: self.description()
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
			url: baseUrl + 'manager/author/delete/' + id,
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
		self.email('');
		self.emailValid(true);
		self.description('');
		self.gravatarUrl('');
		tinyMCE.activeEditor.setContent('');
		$('.collapse').collapse('hide');
		$('.table tr').removeClass('active');
	}

	// Initialze after everything is created.
	self.init();
};
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
// Config view model
//
manager.models.config = function (locale) {
	'use strict';

	var self = window.config = this;

	// Members
	self.active = ko.observable('general');
	self.siteTitle = ko.observable('');
	self.siteTagline = ko.observable('');
	self.siteDescription = ko.observable('');
	self.siteArchivePageSize = ko.observable(0);
	self.siteArchiveTitle = ko.observable('');
	self.siteArchiveKeywords = ko.observable('');
	self.siteArchiveDescription = ko.observable('');
	self.permalinkPage = ko.observable('');
	self.permalinkPost = ko.observable('');
	self.permalinkPostArchive = ko.observable('');
	self.permalinkCategoryArchive = ko.observable('');
	self.permalinkTagArchive = ko.observable('');
	self.cacheExpires = ko.observable(0);
	self.cacheMaxAge = ko.observable(0);
	self.commentModerateAnonymous = ko.observable(false);
	self.commentModerateAuthorized = ko.observable(false);
	self.commentNotifyAuthor = ko.observable(false);
	self.commentNotifyModerators = ko.observable(false);
	self.commentModerators = ko.observable('');

	// Initializes the model
	self.init = function () {
		$.ajax({
			url: baseUrl + 'manager/config/get',
			type: 'GET',
			dataType: 'json',
			success: function (result) {
				if (result.success)
					self.bind(result.data);
			},
			error: function (result) {
				console.log('error');
			}
		});
	};

	// Sets the currently active tab
	self.setActive = function (tab) {
		self.active(tab);
	};

	// Saves the site config
	self.saveSite = function () {
		$.ajax({
			url: baseUrl + 'manager/config/site/save',
			type: 'POST',
			dataType: 'json',
			contentType: 'application/json',
			data: JSON.stringify({
				Title: self.siteTitle(),
				Tagline: self.siteTagline(),
				Description: self.siteDescription()
			}),
			success: function (result) {
				if (result.success) {
					self.bind(result.data);
					manager.notifySave($('#pnlSite'));
				}
			},
			error: function (result) {
				console.log('error');
			}
		});
	};

	// Saves the archive config
	self.saveArchive = function () {
		$.ajax({
			url: baseUrl + 'manager/config/archive/save',
			type: 'POST',
			dataType: 'json',
			contentType: 'application/json',
			data: JSON.stringify({
				Title: self.siteArchiveTitle(),
				PageSize: self.siteArchivePageSize(),
				Keywords: self.siteArchiveKeywords(),
				Description: self.siteArchiveDescription()
			}),
			success: function (result) {
				if (result.success) {
					self.bind(result.data);
					manager.notifySave($('#pnlArchive'));
				}
			},
			error: function (result) {
				console.log('error');
			}
		});
	};

	// Saves the permalink config
	self.savePermalinks = function () {
		$.ajax({
			url: baseUrl + 'manager/config/permalinks/save',
			type: 'POST',
			dataType: 'json',
			contentType: 'application/json',
			data: JSON.stringify({
				PageSlug: self.permalinkPage(),
				PostSlug: self.permalinkPost(),
				PostArchiveSlug: self.permalinkPostArchive(),
				CategoryArchiveSlug: self.permalinkCategoryArchive(),
				TagArchiveSlug: self.permalinkTagArchive()
			}),
			success: function (result) {
				if (result.success) {
					self.bind(result.data);
					manager.notifySave($('#pnlPermalinks'));
				}
			},
			error: function (result) {
				console.log('error');
			}
		});
	};

	// Saves the cache config
	self.saveCache = function () {
		$.ajax({
			url: baseUrl + 'manager/config/cache/save',
			type: 'POST',
			dataType: 'json',
			contentType: 'application/json',
			data: JSON.stringify({
				Expires: self.cacheExpires(),
				MaxAge: self.cacheMaxAge()
			}),
			success: function (result) {
				if (result.success) {
					self.bind(result.data);
					manager.notifySave($('#pnlCache'));
				}
			},
			error: function (result) {
				console.log('error');
			}
		});
	};

	// Saves the comment config
	self.saveComments = function () {
		$.ajax({
			url: baseUrl + 'manager/config/comments/save',
			type: 'POST',
			dataType: 'json',
			contentType: 'application/json',
			data: JSON.stringify({
				ModerateAnonymous: self.commentModerateAnonymous(),
				ModerateAuthorized: self.commentModerateAuthorized(),
				NotifyAuthor: self.commentNotifyAuthor(),
				NotifyModerators: self.commentNotifyModerators(),
				Moderators: self.commentModerators()
			}),
			success: function (result) {
				if (result.success) {
					self.bind(result.data);
					manager.notifySave($('#pnlComments'));
				}
			},
			error: function (result) {
				console.log('error');
			}
		});
	};

	// Saves a config block
	self.saveBlock = function (block) {
		var panel = $('#pnl' + block);
		var input = panel.find('input, textarea');
		var data = [];

		for (var n = 0; n < input.length; n++) {
			data.push({
				Name: $(input[n]).attr('name'),
				Value: $(input[n]).val()
			});
		}

		$.ajax({
			url: baseUrl + 'manager/config/block/save',
			type: 'POST',
			dataType: 'json',
			contentType: 'application/json',
			data: JSON.stringify(data),
			success: function (result) {
				if (result.success) {
					self.bind(result.data);
					manager.notifySave($('#pnl' + block));
				}
			},
			error: function (result) {
				console.log('error');
			}
		});
	};

	// Binds the given data to the model.
	self.bind = function (data) {
		self.siteTitle(data.Site.Title);
		self.siteTagline(data.Site.Tagline);
		self.siteDescription(data.Site.Description);

		self.siteArchiveTitle(data.Archive.Title);
		self.siteArchiveKeywords(data.Archive.Keywords);
		self.siteArchiveDescription(data.Archive.Description);
		self.siteArchivePageSize(data.Archive.PageSize);

		self.permalinkPage(data.Permalinks.PageSlug);
		self.permalinkPost(data.Permalinks.PostSlug);
		self.permalinkPostArchive(data.Permalinks.PostArchiveSlug);
		self.permalinkCategoryArchive(data.Permalinks.CategoryArchiveSlug);
		self.permalinkTagArchive(data.Permalinks.TagArchiveSlug);

		self.cacheExpires(data.Cache.Expires);
		self.cacheMaxAge(data.Cache.MaxAge);

		self.commentModerateAnonymous(data.Comments.ModerateAnonymous);
		self.commentModerateAuthorized(data.Comments.ModerateAuthorized);
		self.commentNotifyAuthor(data.Comments.NotifyAuthor);
		self.commentNotifyModerators(data.Comments.NotifyModerators);
		self.commentModerators(data.Comments.Moderators);

		for (var n = 0; n < data.Params.length; n++) {
			$('#' + data.Params[n].Name).val(data.Params[n].Value);
		}
	};

	// Initialze after everything is created.
	self.init();
};
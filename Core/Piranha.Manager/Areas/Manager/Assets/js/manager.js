//
// Copyright (c) 2014-2015 Håkan Edling
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
// 
// http://github.com/piranhacms/piranha.vnext
// 

//
// Char counter
$(document).on('keyup', '.count-me', function () {
	var length = $(this).val().length;
	$(this).next().find('span').text(length);
});

//
// Save notification
$(document).ready(function () {
	var body = $('body');

	if (body.hasClass('pre-saved')) {
		manager.notifySave(body);
	}
});

// 
// Create base object
var manager = {};

//
// Executes a save notification on the given element.
manager.notifySave = function (elm) {
	// Handle save notifications
	elm.addClass('pre-saved');
	setTimeout(function () {
		elm.removeClass('pre-saved').addClass('saved');
		setTimeout(function () {
			elm.removeClass('saved').addClass('post-saved');
			setTimeout(function () {
				elm.removeClass('post-saved');
			}, 400);
		}, 800);
	}, 200);
};

//
// Comment dialog handler
manager.commentDialog = function (domElm, baseUrl, postId) {
	'use strict';

	var self = this;

	self.domElm = domElm;
	self.baseUrl = baseUrl;
	self.postId = postId;

	$(document).on('change', self.domElm + ' .comment-approved', function () {
		var row = $(this).parent().parent();
		var commentId = $(row).find('.comment-id').val();

		$.ajax({
			url: baseUrl + '/approve',
			type: 'POST',
			dataType: 'html',
			contentType: 'application/json',
			data: JSON.stringify({
				PostId: self.postId,
				CommentId: commentId,
				Status: $(this).is(':checked')
			}),
			success: function (result) {
				$(domElm).html(result);
			}
		});
	});

	// Updated spam status for comment
	$(document).on('change', self.domElm + ' .comment-spam', function () {
		var row = $(this).parent().parent();
		var commentId = $(row).find('.comment-id').val();

		$.ajax({
			url: self.baseUrl + '/spam',
			type: 'POST',
			dataType: 'html',
			contentType: 'application/json',
			data: JSON.stringify({
				PostId: self.postId,
				CommentId: commentId,
				Status: $(this).is(':checked')
			}),
			success: function (result) {
				$(domElm).html(result);
			}
		});
	});
};
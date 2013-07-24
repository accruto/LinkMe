(function (linkme, $, undefined) {

    linkme.members = linkme.members || {};
    linkme.members.jobads = linkme.members.jobads || {};

    linkme.members.jobads.api = (function () {

        var _urls = null;

        return {

            setUrls: function (urls) {
                _urls = urls;
            },

            addJobAdsToMobileFolder: function (jobAdIds, onSuccess) {
                var data = { jobAdIds: jobAdIds };
                linkme.api.post(_urls.addJobAdsToMobileFolder, data, onSuccess);
            },
            
            removeJobAdsFromMobileFolder: function(jobAdIds, onSuccess) {
                var data = { jobAdIds: jobAdIds };
                linkme.api.post(_urls.removeJobAdsFromMobileFolder, data, onSuccess);
            }

        };

    } ());
} (window.linkme = window.linkme || {}, jQuery));

 

 

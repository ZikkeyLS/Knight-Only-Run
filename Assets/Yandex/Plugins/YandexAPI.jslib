mergeInto(LibraryManager.library, {
    ShowBannerAds: function() {
        // myGameInstance.SendMessage('GameData', 'ShowBannerAds', args);
    },

    RateGame: function() {
        ysdk.feedback.canReview()
            .then(({ value, reason }) => {
                if (value) {
                    ysdk.feedback.requestReview()
                        .then(({ feedbackSent }) => {
                            console.log(feedbackSent);
                        })
                } else {
                    console.log(reason)
                }
            })
    },

    Init: function() {
        initPlayer();
        // Send init state
    },

    Auth: function() {
        // Send auth state
        if (player.getMode() === 'lite') {
            // Not authorized.
            ysdk.auth.openAuthDialog().then(() => {
                    // Authorized successfully.
                    initPlayer().catch(err => {
                        // Error on player init.
                    });
                }).catch(() => {
                    // Not authorized for a reason
                });
        }
    },

    SaveExtern: function(data) {
        var dataString = UTF8ToString(data);
        var myobj = JSON.parse(dataString);
        player.setData(myobj);
    },

    LoadExtern: function() {
        player.getData().then(_data => {
            const myJSON = JSON.stringify(_data);
            myGameInstance.SendMessage('GameData', 'SetPlayerInfo', myJSON);
        });
    },

    GameReady: function() {
        ysdk.features.LoadingAPI.ready();
    },
    
    ShowStickyAd: function() {
        ysdk.adv.showBannerAdv()
    },

    HideStickyAd: function() {
        ysdk.adv.hideBannerAdv()
    },

    ShowInterstitialAd: function() {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: function(wasShown) {
                // some action after close
                },
                onError: function(error) {
                // some action on error
                }
            }
        })
    },

    SetLeaderboardScore: function(name, score) {
        var nameString = UTF8ToString(name);

        ysdk.getLeaderboards()
            .then(lb => {
                lb.setLeaderboardScore(nameString, score);
            });
    },
});

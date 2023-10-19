mergeInto(LibraryManager.library, {
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

    Auth: function() {
        initPlayer().then(_player => {
                if (_player.getMode() === 'lite') {
                    // The player is not logged in.
                    ysdk.auth.openAuthDialog().then(() => {
                            // The player is successfully logged in
                            myGameInstance.SendMessage('GameData', 'SetAuthorized');

                            initPlayer().catch(err => {
                                // Error initializing the Player object.
                            });
                        }).catch(() => {
                            // The player is not logged in.
                        });
                }
                else {
                    myGameInstance.SendMessage('GameData', 'SetAuthorized');
                }
            }).catch(err => {
                // Error initializing the Player object.
            });
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

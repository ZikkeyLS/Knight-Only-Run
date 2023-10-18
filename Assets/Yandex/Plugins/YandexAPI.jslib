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

    Init: function {
        initPlayer();
        // Send init state
    }

    Auth: function() {
        // Send auth state
        if (_player.getMode() === 'lite') {
            // Игрок не авторизован.
            ysdk.auth.openAuthDialog().then(() => {
                    // Игрок успешно авторизован
                    initPlayer().catch(err => {
                        // Ошибка при инициализации объекта Player.
                    });
                }).catch(() => {
                    // Игрок не авторизован.
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

});

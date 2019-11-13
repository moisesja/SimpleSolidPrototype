﻿async function showSolidLogin() {
    
    const popupUri = '/lib/solid-auth-client/dist-popup/popup.html';
    const session = await solid.auth.popupLogin({ popupUri });

    if (session)
        window.location.replace(homeRedirectUrl);
}

$(function ($) {

    let authorizationToken = '';

    $('#logout-button').hide();
    $('#login-button').click(showSolidLogin);

    solid.auth.trackSession(session => {

        console.log('Moises say: Here\'s the session.', session);

        const loggedIn = !!session;
        
        $('#login-button').toggle(!loggedIn);
        $('#logout-button').toggle(loggedIn);

        if (session) {

            authorizationToken = session.authorization.id_token;
            console.log('Token:', authorizationToken);
            console.log('Web ID:', session.webId);

            // Fetch the contents from the Private Folder
            solid.auth.fetch('https://moisesj.inrupt.net/inbox')
                .then(response => {

                    if (!response.ok)
                        throw response;

                    console.log('Response Body', response.text());
                });
        }
        else {
            console.log('Moises say: No session data.');
        }
    });

    $('#logout-button').click(() => solid.auth.logout());    
});
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function gameBl(allRecordDecorators, gameListId, config) {


    //#region Fields
    var gameRecordDecorators;
    var recordToGuess;
    var _wrongGuesses = 0;
   
    const labelRecordToGuessId = "labelRecordToGuess";
    const h1GameOverId = "h1GameOver";
    const gameOverSpanId = "gameOverSpan";
    const h1ResultId = "h1Result";  
    const spanResultId = "spanResult";
    const recordScoreId = "recordScore";
    const guessedCounterSpanId = "guessedCounterSpan";
    const lifesSpanId = "lifesSpan";
    const gameDefaultButtonStyleName = "btn btn-dark btn-lg btn-block submit";
    const gameWrongButtonStyleName = "btn btn-danger btn-lg btn-block submit";
    const gameRightButtonStyleName = "btn btn-success btn-lg btn-block submit";
   
    //#endregion

    //#region UI

    function updateElementHtmlById(id, text) {
        var elem = document.getElementById(id);
        elem.innerHTML = text;
        return elem;
    }

    function clearElement(elem) {
        while (elem.firstChild) {
            elem.removeChild(elem.firstChild);
        }
    }

    function animateElementById(id, blinkAnDelay, fadeOutAnDelay) {
        $("#" + id).blink({ delay: blinkAnDelay });
        $("#" + id).fadeOut(fadeOutAnDelay);
    }

    function disableElement(id) {
        var elem = document.getElementById(id);
        elem.disabled = true;
    }

    function updateElementCssClass(id, className) {
        const elem = document.getElementById(id);
        elem.className = className;
        return elem;
    }

    function createElement(id, type) {
        const elem = document.createElement(type);
        elem.id = id;
        return elem;
    }

    function onHandleClick() {
        var resultContentDiv = document.getElementById("resultContent");
        //clear all result labels
        clearElement(resultContentDiv);
        //generate new result label
        const h1 = createElement(h1ResultId, "h1");
        h1.appendChild(createElement(spanResultId, "span"));
        // add them to the screen
        resultContentDiv.appendChild(h1);
        //add animation to result label
        animateElementById(h1ResultId, config.blinkDelay, config.fadeOutDelay);
        // make buttons disabled to avoid redundunt clicking
        for (var i = 0; i < gameRecordDecorators.length; i++) {
            disableElement(gameRecordDecorators[i].id);
        }
        //right guess - make the right button green anyway
        updateElementCssClass(recordToGuess.id, gameRightButtonStyleName);
        var count;

        if (recordToGuess.id == this.id) {//correct
            //mark record wheather it was guessed or not, to exclude this record from the game
            //update isGuessed state to "true" should be before counting of getAllGuessedCount
            updateAllRecordsState(allRecordDecorators, parseInt(this.id), true);
            count = getAllGuessedCount(allRecordDecorators);
            console.log("Your score is " + count);
            updateElementHtmlById(guessedCounterSpanId, "Your score is " + count);
            updateElementCssClass(spanResultId, "label label-success").innerHTML ="&#x1F603; " +" Correct!";//happy emoji

            if (count == allRecordDecorators.length) {//you won!
                console.log("game over you won. count is " + count + " allRecordDecorators.length is " + allRecordDecorators.length)
                endTheGame(true);
                return;
            }
        }
        else {//wrong
            updateElementCssClass(this.id, gameWrongButtonStyleName)
            _wrongGuesses++;
            const lifesSpan = updateElementHtmlById(lifesSpanId, config.maxWrongGuesses - _wrongGuesses);

            if (config.maxWrongGuesses === _wrongGuesses + 1) {//last life
                lifesSpan.className = "lastLife";
            }
            updateElementCssClass(spanResultId, "label label-danger").innerHTML ="&#x1F622; "+ "Wrong!";//unhappy emojy

            if (config.maxWrongGuesses === _wrongGuesses) {//game over
                console.log("game over you loose. count is " + count + " allRecordDecorators.length is " + allRecordDecorators.length)
                endTheGame(false);
                return;
            }
        }

        const gameParameters = buildGameParameters(allRecordDecorators);
        recordToGuess = gameParameters.recordToGuess;
        //build screen with the same delay as correct/wrong label disappears to show that to the user
        window.setTimeout(function () {
            buildScreen(gameParameters.gameArr);
        }, config.fadeOutDelay);

    }

    function initGameParams() {
        var recordScoreH1 = document.getElementById(recordScoreId);
        recordScoreH1.innerHTML = "Your record is " + config.bestScore;

        console.log("allRecordDecorators are " + JSON.stringify(allRecordDecorators));
        const gameParameters = buildGameParameters(allRecordDecorators);
        recordToGuess = gameParameters.recordToGuess;
        gameRecordDecorators = gameParameters.gameArr;
        console.log("gameRecordDecorators are " + JSON.stringify(gameRecordDecorators));
        console.log("recordToGuess is " + JSON.stringify(recordToGuess));

        updateElementHtmlById(guessedCounterSpanId, "Your score is 0");
        updateElementHtmlById(lifesSpanId, config.maxWrongGuesses);
    }

    function addElemToElem(parentId, elemId, elemValue, elemType, elemCss) {
        var element = document.createElement("input");
        //Assign different attributes to the element.
        element.setAttribute("type", elemType);
        element.setAttribute("class", elemCss);
        element.setAttribute("value", elemValue);
        element.setAttribute("id", elemId);
        var content = document.getElementById(parentId);
        //Append the element in page (in span).
        content.appendChild(element);
        content.appendChild(document.createElement("br"));
    }

    function buildScreen(gameArr) {
        //clear the screen and define new records
        var contentDiv = document.getElementById("content");
        clearElement(contentDiv);

        //initializing - first time
        if (typeof gameArr === 'undefined' || gameArr === null) {
            initGameParams();
        }
        else {
            gameRecordDecorators = gameArr;
        }
        //if isFromForeignLanguage==true, update labelRecordToGuess with word, otherwise with translation
        document.getElementById(labelRecordToGuessId).innerHTML = config.isFromForeignLanguage === true ?
            recordToGuess.word : recordToGuess.translation;

        for (var i = 0; i < gameRecordDecorators.length; i++) {
            const buttonContent = config.isFromForeignLanguage === true ?
                gameRecordDecorators[i].translation : gameRecordDecorators[i].word;
            addElemToElem("content", gameRecordDecorators[i].id, buttonContent, "button",
                gameDefaultButtonStyleName);
        }
        //describe in report problem with click
        $('.submit').click(onHandleClick);//add click handler to all buttons that have "submit" in css class name
    }

    //#endregion

    //#region BL
    function updateAllRecordsState(arr, id, state) {
        for (var i = 0; i < arr.length; i++) {
            if (id === arr[i].id) {
                arr[i].isGuessed = state;
            }
        }
    }

    //excludeArr - array for elements excluded from generation and gathering
    function getRandomArr(arr, min, max, count, excludeArr) {
        if (typeof arr === 'undefined' || arr === null) {
            arr = [];
            console.log('arr: ' + arr);
        }
        var randNum = Math.floor(Math.random() * (max - min + 1)) + min;

        if (!arr.includes(randNum) && !excludeArr.includes(randNum))
            arr.push(randNum);
        //continue recursion call untill will be enough elements
        if (arr.length < count)
            return getRandomArr(arr, min, max, count, excludeArr);
        else
            return arr;
    }

    //call to the default POST method of the pages
    function post(config, listId) {
        document
            .forms[0].action = "Game?configId=" + config.id + "&recordScore=" +
            config.bestScore + "&userId=" + config.userId + "&listId=" + listId;
        document.forms[0].submit();
    }
    //end the game and redirect to the post method
    function endTheGame(isWon) {
        var contentDiv = document.getElementById("content");
        clearElement(contentDiv);
        $("#" + h1GameOverId).show();

        var allGuessedCount = getAllGuessedCount(allRecordDecorators);
        if (isWon) {
            updateElementCssClass(gameOverSpanId, "label label-success").innerHTML =
                "You won! Your score is " + allGuessedCount;
            config.bestScore = allGuessedCount;

            window.setTimeout(function () {
                post(config, gameListId);
            }, config.redirectionDelay);
        }
        else {
            updateElementCssClass(gameOverSpanId, "label label-danger").innerHTML =
                "Game Over. Your score is " + allGuessedCount + ".";
        }
    }

    function getAllGuessedIndexes(arr) {
        var indexes = [];
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].isGuessed === true) {
                indexes.push(i);
            }
        }
        return indexes;
    }

    function getAllGuessedCount(arr) {
        var count = 0;
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].isGuessed === true) {
                count++;
            }
        }
        return count;
    }

    function buildGameArray(arr, indexesArr) {
        var gameArr = [];
        for (var i = 0; i < indexesArr.length; i++) {
            for (var j = 0; j < arr.length; j++) {
                if (j === indexesArr[i]) {
                    gameArr.push(arr[j]);
                }
            }
        }
        return gameArr;
    }

    function buildGameParameters(allRecords) {
        const guessedIdexes = getAllGuessedIndexes(allRecords);

        const indexGuessRecordUnique = getRandomArr(null, 0, allRecords.length - 1, 1,
            guessedIdexes)[0];
        const indexsForGameRecords = getRandomArr(null, 0, allRecords.length - 1, config.buttonsNumber,
            [indexGuessRecordUnique]);

        const indexForGameRecordInGameArray = getRandomArr(null, 0, 2, 1, [])[0];

        indexsForGameRecords[indexForGameRecordInGameArray] = indexGuessRecordUnique;

        return {
            "gameArr": buildGameArray(allRecords, indexsForGameRecords),
            "recordToGuess": allRecords[indexGuessRecordUnique]
        };

    }

    //#endregion

    $(window).on('load', buildScreen(null));


}

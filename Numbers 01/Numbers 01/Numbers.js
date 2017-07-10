// Use this jquery method call to halt execution of JavaScript until the entire document is loaded.

$(document).ready(function () {

    // Add a handler for each button within a <form> tag to prevent the default behavior for the click event.
    // Replace the id in the jquery selector with the id of your button
    $("#calculate").click(function (event) {
        event.preventDefault();
    });

    $("#calculateFactorial").click(function (event) {
        event.preventDefault();
    });

    $("#calculateFizzBuzz").click(function (event) {
        event.preventDefault();
    });

    $("#calculatePalindrome").click(function (event) {
        event.preventDefault();
    });

    // BEGIN JavaScript

    //****************** Number Calculator ************************************

    // Initialize Values

    $("#calculate").click(calcNumbers);
    function calcNumbers() {
        var firstNum = 0;
        var secondNum = 0;
        var thirdNum = 0;
        var fourthNum = 0;
        var fifthNum = 0;

    // Parse String Number Data into Number Data

        var firstNum = parseInt($('#first').val());
        var secondNum = parseInt($('#second').val());
        var thirdNum = parseInt($('#third').val());
        var fourthNum = parseInt($('#fourth').val());
        var fifthNum = parseInt($('#fifth').val());

    // Calculate Numbers

        var minimum = Math.min(firstNum, secondNum, thirdNum, fourthNum, fifthNum);
        var maximum = Math.max(firstNum, secondNum, thirdNum, fourthNum, fifthNum);
        var total = firstNum + secondNum + thirdNum + fourthNum + fifthNum;
        var array = [firstNum, secondNum, thirdNum, fourthNum, fifthNum];
        var average = total / array.length;
        var multiply = firstNum * secondNum * thirdNum * fourthNum * fifthNum;

    // Return Numbers

        $('#least').html(minimum);
        $('#greatest').html(maximum);
        $('#sum').html(total);
        $('#mean').html(average);
        $('#product').html(multiply);
    }


    //****************** Factorial ************************************

    // Initialize Value

    $('#calculateFactorial').click(calcFactorial);
    function calcFactorial() {
        var factNum = 0;
    
    // Parse String Number Data into Number Data

        var factNum = parseInt($('#number1').val());

    // Calculate Factorial

        var result = 1;

        for (var i = factNum; i > 0; i--) {
            result = result * i;
        };

    // Return Numbers

        $('#factorialOutput').html(result)
    }

    //****************** Fizz Buzz ************************************

    // Initialize Value

    $('#calculateFizzBuzz').click(calcFizzBuzz);
    function calcFizzBuzz() {
        var fbNum1 = 0;
        var fbNum2 = 0;

    // Clear previous answers

        $('#fizzBuzz100').html('')
        $('#fizzBuzzOutput').html('')

    // Fizz Buzz 1 - 100

        for (var i = 1; i <= 100; i++) {
            $('#fizzBuzz100').append('<br>' + i);
        }

    // Parse String Number Data into Number Data

        var fbNum1 = parseInt($('#fizzBuzz1').val());
        var fbNum2 = parseInt($('#fizzBuzz2').val());

    // Calculate Fizz Buzz

        for (var i = 1; i <= 100; i++) {
            if (i % fbNum1 !== 0 && i % fbNum2 !== 0)
                // If modulus returns a remainder for both user numbers - return 1-100 number
                $('#fizzBuzzOutput').append('<br>' + i);
            else if (i % fbNum1 === 0 && i % fbNum2 === 0)
                // If modulus returns NO remainder for both user numbers - return "Fizz Buzz"
                $('#fizzBuzzOutput').append('<br>' + 'Fizz Buzz');
            else if (i % fbNum1 === 0)
                // If modulus returns NO remainder for first user number - return "Fizz"
                $('#fizzBuzzOutput').append('<br>' + 'Fizz');
            else if (i % fbNum2 === 0)
                // If modulus returns NO remainder for second user number - return "Buzz"
                $('#fizzBuzzOutput').append('<br>' + 'Buzz');
        }
    }

    //****************** Palindrome ************************************

    // Initialize Value

    $('#calculatePalindrome').click(calcPalindrome);
    function calcPalindrome() {
        var palWord = '';

    // Get Palindrome Word from Form Data

        var palWord = $('#palindromeWord').val();

    // Break apart string, reverse it, join back together and determine if palindrome

            // Remove spaces from string
        var noSpace = palWord.split(' ').join('');
            // Split string into array of separate characters
        var splitWord = noSpace.split('');
            // Reverse the array
        var reverseWord = splitWord.reverse();
            // Join the characters from array back to a string
        var joinWord = reverseWord.join('');
            // Determine if palindrome and return result
        if (noSpace === joinWord) {
            $('#palindromeOutput').html('Congratulations, you\'ve discovered a palindrome!');
        } else {
            $('#palindromeOutput').html('Sorry, this word is not a palindrome.');
        }
    }
});
    // END JavaScript

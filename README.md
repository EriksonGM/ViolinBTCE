ViolinBTCE
==========
<p align="right">
<a href="http://www.appveyor.com/"><img alt="Tests status" src="https://ci.appveyor.com/api/projects/status/u76hesrmw3rgywoy"/></a></p>
## Synopsis

This project aims to offer an API which is easy to use and ensure behavior by testing all of the code. 

This project is a fork of https://github.com/DmT021/BtceApi . It has been cleaned up and used some new techniques for implementation. Unit tests are now being written to ensure 100% of expected behavior.

## Code Example

``` c#
ViolinBtce violinBtce = new ViolinBtce("key","secret");

  DtoUserInfo info = violinBtce.GetInfo();

  decimal balance = violinBtce.GetBalance(Currency.usd);

  decimal fee = violinBtce.GetFee(Pair.eur_usd);

  DtoTicker ticker = violinBtce.GetTicker(Pair.eur_usd);

  DtoTradeAnswer tradeAnswer = violinBtce.Trade(Pair.ltc_eur, TradeType.sell, 100m, 0.1m);
```

*More Operations will be added asap.

## Motivation

I decided to have a clean, easy to use API in order to create scripts to trade on the BTC-E market, so that I didn't need to worry about the API i was using. The old API offered by DmT021 is great, but did not make be comfortable to use it without eing sure it works as I expected.

## Installation

Add the projects to your solution and start using the sintaxes shown on "Code examples". You can also compile the project and reference it in your project (more comfortable).

## API Reference

I'll create an API Reference page as soon as it's finished and tested.

## Tests

Tests status: <a href="http://www.appveyor.com/"><img alt="Tests status" src="https://ci.appveyor.com/api/projects/status/u76hesrmw3rgywoy"/></a>

Tests coverage 21st of August, 2014:
![alt tag](https://raw.githubusercontent.com/brunoamancio/ViolinBTCE/master/ViolinBTCE.Test/ViolinBtce_TestsCoverage_2014_08_21.png)

To run the tests, you can use NUnit GUI, for example. I use resharper to run it and dotcover to detect what is being covered or not.

## Contributors

Th3B0Y (me). If you want to contribute, open an issue and/or send a pull request. It will be analysed and published, if approved. Your name/nickname will be shown here.

## License

<a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/"><img alt="Creative Commons Licence" style="border-width:0" src="https://i.creativecommons.org/l/by-nc/4.0/88x31.png" /></a><br /><span xmlns:dct="http://purl.org/dc/terms/" property="dct:title">ViolinBTCE</span> by <a xmlns:cc="http://creativecommons.org/ns#" href="https://github.com/brunoamancio/ViolinBTCE" property="cc:attributionName" rel="cc:attributionURL">Th3B0Y</a> is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/">Creative Commons Attribution-NonCommercial 4.0 International License</a>.<br />Based on a work at <a xmlns:dct="http://purl.org/dc/terms/" href="https://github.com/DmT021/BtceApi" rel="dct:source">https://github.com/DmT021/BtceApi</a>.






# ButtFish 🍑🐟
### Effortlessly transmitting Morse Code of chess moves to your butthole 💝
---

## Crypto

- [There is a SOL based Buttfish coin, and I'm getting creators fees](https://pump.fun/coin/58Ghft9TvGNuBiDzy9cFD2Px9rz5cDVZ1XoMFk1xpump)
- CA: 58Ghft9TvGNuBiDzy9cFD2Px9rz5cDVZ1XoMFk1xpump
- Also http://buttfi.sh goes there
- So if you're trading memecoins anyways, this is getting me free SOL :)

## Web

Buttfish has been deployed as a website - see: https://ronsijm.github.io/ButtFish/
![WebPreview](https://github.com/user-attachments/assets/a95c5f87-b204-4124-bd82-57572144b0a4)

Game modes:
- "Free Play" - Same as the original ButtFish. You can play, get a next 'best engine move' suggestion
- "AI Mode" - Play against an AI (Normal)
- "Play vs AI - Blind Mode"
-- in Blind Mode you do not see the AI's chess pieces. It's sort of like blindfolded chess, but you have to fully rely on good vibes (literally)

## Wait what now?

If you haven't been following the chess news - (because why would you) -
As of the last couple of weeks - (Monday September 5, 2022) there's been drama going on in the chess world.

During the Sinquefield Cup there was a chess match between 5-time world chess champion Magnus Carlsen - (basically the final boss of chess) - and another lower level chess player. No one expected Magnus to lose, but he did. After losing the chess match Magnus Carlsen withdrew from the tournament and posted a tweet saying "I've withdrawn from the tournament" and attached a meme video of Jose Mourinho saying ["I prefer not to speak. If I speak I am in big trouble … and I don't want to be in big trouble."](https://www.youtube.com/watch?v=yogfwJXVBDg) - Classic 👌.

The tweet was interpreted by the chess community as an accusation of cheating.

This chess drama has been ongoing for the last couple weeks with everyone making wild guesses on how the alleged cheating could have been done.

The wildest accusation is that the cheating player might have been using a Buttplug - sending him information about the best moves though his butthole

I'm not going to name the other player, google it I suppose. So far it hasn't been proven that he actually cheated, though he has received a lot of backlash already - so I assume this whole drama has been a huge pain in the butt for him already anyways. 🥁

---

## Why?

This project is mostly a meme.

Firstly I didn't have much else to do this weekend, so it seemed like a fun thing to implement. I could not find any project making this kind of cheat possible. The closest mention I could find is [Sockfish](https://incoherency.co.uk/blog/stories/sockfish.html) \- A project by James Stanley where he implemented Stockfish in his shoes (or sock). I'm not entirely sure how he implemented it, or whether his implementation is open sourced.

So I started looking into the possibilities and I found the project [Buttplug.io](https://buttplug.io/) \- the name of this project is not super inclusive of what it actually does - actually it connects to a huge amount of sex toys. [See a full list here \[nsfw\]](https://iostindex.com/?filter0Availability=Available,DIY&filter1Connection=Digital). Other devices include Cockthings, so if you're not comfortable with putting Stockfish in your butthole, there's also the possibility of trying this with a Cockthing, and practically you'll be running *CockFish*.

Secondly, who else would possibly build this? I've created a Venn diagram to illustrate how rare the chances are someone would develop this.

![venndiagram](https://user-images.githubusercontent.com/337928/192335511-f8a3d559-ad29-443b-ab3f-c45125b1e100.png)

As you can see by the diagram, if we'd have to wait for "the chosen one" with interests in all three fields, this project would never exist. So I decided that it should exist, so I made it.

---

## Features

#### Connectivity

- [x] Discover Yeelight devices on your network
- [x] Manually connect with Yeelight devices on your network though IP or hostname
- [x] Discover Buttplug.io Capable devices on your network
- [x] Manually connect with Buttplug.io Capable devices though remote discover host address

#### Main

- [x] Read a Chess FEN string and determine the next best move
- [x] Encode the next best move into Morse Code
- [x] Send the encoded Morse Code to the connected device

#### Encoders

- [x] (default) [A text to Morse Encoder](https://github.com/RonSijm/ButtFish/blob/main/RonSijm.ButtFish/Encoders/MorseEncoder.cs)
- [x] [A simplified Pulse Encoder](https://github.com/RonSijm/ButtFish/blob/main/RonSijm.ButtFish/Encoders/SimplifiedPulseEncoder.cs) 
   - `appsettings.json` -> Encoder -> "SimplifiedPulse"

#### Config

- [x] End Position Only [Expert] By just knowing the end position, a serious chess player would know intuitively which piece belongs there. ~ [Hikaru](https://youtu.be/ifJnWVSoyAY?t=431)
   - `appsettings.json` -> EndPositionOnly -> true (default false)
- [x] Use Manual Input - Skip using a chess engine, and manually send moves to devices. Allows you to manually input moves to broadcast instead
   - `appsettings.json` -> UseManualInput -> true (default false)
 
#### Tested and Supported Engines

- [x] Stockfish (stockfish_15_x64_avx2.exe)
- [x] Leela Chess Zero (Lc0) (lc0-v0.28.2-windows-gpu-nvidia-cuda)

Technically any UCI compatible chess engine should work. But I haven't tested others yet.

* * *

### Demo

#### Discovery

A demo of discovering devices on the network
![discovery](https://user-images.githubusercontent.com/337928/192335028-c3ffd6b0-3b96-49c1-be87-f95cd91a8ede.gif)

#### Manual

A demo of manually entering a Yeelight address
![manual](https://user-images.githubusercontent.com/337928/192335161-9cd095a1-133b-4b0a-930b-e5174c1aa7b5.gif)

#### Buttplug.io side by side

The internet pointed out that I hadn't actually tested it out with a vibrating device. 
Also there's a theory in computer science that [every programmer needs a rubby ducky](https://rubberduckdebugging.com/) - [for debugging purposes.](https://en.wikipedia.org/wiki/Rubber_duck_debugging). What I found is close enough

After scrolling through the list of compatible Buttplug.io devices, I came across this nonsensical device
![ducky](https://user-images.githubusercontent.com/337928/192588741-dc7f6268-c87f-4890-a335-976910adda0e.png)

It reminds me of a real-life representation of the famous internet meme dickbutt. 

I also don't have a rubber duck for debugging purposes yet, so I count this as a win-win-win.

So heres a demo of a Buttplug.io connected to Libo Carlos - side by side with the application

[sidebyside-butplugio.webm](https://user-images.githubusercontent.com/337928/204144604-b0513f22-c1d4-43f8-9881-f65ebcddca69.webm)

#### Yeelight side by side

A demo of a Yeelight connector - side by side with the application

[sidebyside-yeelight.webm](https://user-images.githubusercontent.com/337928/192337315-964201e4-0a23-49bc-8889-a079357967c3.webm)

---

### Setup / Yeelight

To use a Yeelight, you must enable developer mode for your Yeelight. To do so, do the following:

- Open Yeelight APP and go to “Device”.
- Select the device you want to use.
- Enable LAN Control
- (optional), if network discovery of the Yeelight does not seem to work:)
- Go to your Yeelight settings in the upper right corner
- Click Device Info
- You should now see a field "IP address". Use the value for manual mode.

---

### Data

Example FEN code that I used: *"rnb1kbnr/pppp1ppp/8/4p1q1/5P2/4PQ2/PPPP2PP/RNB1KBNR b KQkq - 2 3"*

If you want to generate a FEN code on chess.com, you can go to a match, click on analysis - which will redirect you to https://www.chess.com/analysis with the selected match, and then if you press the share button you will see a FEN code. There might be an easier way to do this, but I don't know

---

### TODO

- [ ] **Real world chess match testing** \- The chess world seems to assume that it's possible to accurately receive chess moves through your butthole, and use this to your advantage and even beat the final chess boss Magnus Carlsen with this. Since this has been a working theory, so far I haven't seen any attempts to replicate this. Hopefully this project with aid in this endeavor. Unfortunately I can barely play chess, nor understand Morse Code, so hopefully we'll be able to find some brave chessmaster grandmaster that's willing to 'take one for the team' (in the butt)
- [ ] **Post vid** In the spirit of [WatchPeopleCode](https://www.reddit.com/r/WatchPeopleCode/) I recorded my development process. I haven't had time to edit it down and post it. Might if someone is actually interested (probably not)

---

## Donate

I don't really need your money, but it would be cool to see that 0 turn into a 1: 

[![](https://img.shields.io/github/sponsors/ronsijm?label=Sponsor&logo=GitHub)](https://github.com/sponsors/ronsijm)

---

## Code of Conduct and Ethics

As mentioned before - this project is a meme. I do not endorse or encourage chess cheating in any way possible. Would you decide to try out this project yourself, you must notify your chess partner in advance that you're using chess augmentations though vibrations in your butthole.

Would you decide not to disclose this in (official) matches, then I'm in no way responsible for the consequences of either you getting banned from chess.com, or pissing off Magnus Carlsen and having him forfeit your matches after a couple of moves

---

### Coverage and Mentions (incoming)

[This project was covered by](https://www.vice.com/en/article/5d3w9z/did-hans-neimann-cheat-at-chess-with-a-sex-toy-this-coder-is-attempting-to-find-out) [Matthew Gault](https://www.vice.com/en/contributor/matthew-gault) a Staff Writer at Motherboard/VICE Media.
Initially it was on the front-page of Motherboard, on their front-page together with a live feed of NASA's DART mission - humanity's first strike against potential total annihilation by an asteroid in the future. 

![vice-front-page](https://user-images.githubusercontent.com/337928/192803688-06c840bd-352a-4e12-8c6f-49f187dd21b8.png)

Someone is solving possibly the biggest crisis in human history, while the other is just smashing spacecrafts into a space rocks

NASA and I have different priorities I suppose...

- [It was also shown on the VICE front-page of World News...](https://user-images.githubusercontent.com/337928/192804145-49200e59-77b0-4d8d-b5f0-a9ca11fdfe20.png)
- [Hikaru Nakamura](https://www.youtube.com/watch?v=ifJnWVSoyAY) Five-time U.S. Chess Champion and Chess Meme King himself read the vice article on youtube
- [Gigazine.net](https://gigazine.net/news/20220929-buttfish/) covered it in Japanese [[Google translate English](https://gigazine-net.translate.goog/news/20220929-buttfish/?_x_tr_sl=ja&_x_tr_tl=en)]
- [Sohu.com](https://www.sohu.com/a/588661192_610300) covered it in Chinese [[Google translate English](https://www-sohu-com.translate.goog/a/588661192_610300?_x_tr_sl=zh-CN&_x_tr_tl=en&_x_tr_hl=en)]
![Sohucom2](https://user-images.githubusercontent.com/337928/193144038-f80dddbb-afd9-40ab-86db-7bff14a20494.png)

### Library and Mentions (outgoing)

- Buttplug.io main website: https://buttplug.io
- Buttplug.io C# library: https://github.com/buttplugio/buttplug-rs-ffi/tree/master/csharp
- Stockfish main website: https://stockfishchess.org
- Stockfish C# library: https://github.com/Oremiro/Stockfish.NET
- Yeelight C# library: https://github.com/roddone/YeelightAPI
- ColorfulConsole C# library: https://github.com/tomakita/Colorful.Console

 ## Contact and questions:
I've created a Discord, though now it's just me, lol.. https://discord.gg/cDC6VkUn2X - but you can ask stuff here

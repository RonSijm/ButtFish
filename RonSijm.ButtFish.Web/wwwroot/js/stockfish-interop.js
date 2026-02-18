// Stockfish WASM interop for Blazor
// This module provides a bridge between Blazor C# and Stockfish WASM

let stockfish = null;
let dotNetHelper = null;
let isReady = false;

export async function initializeStockfish(dotNetRef) {
    try {
        dotNetHelper = dotNetRef;

        // Create a Web Worker for Stockfish
        // Use the lite single-threaded version for better compatibility
        const workerPath = new URL('../lib/stockfish/stockfish-18-lite-single.js', import.meta.url);
        stockfish = new Worker(workerPath);

        // Set up message handler
        stockfish.onmessage = function(event) {
            const message = event.data;

            if (message === 'uciok') {
                isReady = true;
            }

            // Parse best move
            if (message.startsWith('bestmove')) {
                const parts = message.split(' ');
                if (parts.length >= 2) {
                    const move = parts[1];
                    if (dotNetHelper) {
                        dotNetHelper.invokeMethodAsync('OnBestMove', move);
                    }
                }
            }

            // Parse info lines for evaluation
            if (message.startsWith('info') && message.includes('score')) {
                if (dotNetHelper) {
                    dotNetHelper.invokeMethodAsync('OnEngineInfo', message);
                }
            }
        };

        stockfish.onerror = function(error) {
            console.error('Stockfish worker error:', error);
        };

        // Initialize UCI
        stockfish.postMessage('uci');

        // Wait for ready
        await new Promise((resolve) => {
            const checkReady = setInterval(() => {
                if (isReady) {
                    clearInterval(checkReady);
                    resolve();
                }
            }, 100);
        });

        return true;
    } catch (error) {
        console.error('Failed to initialize Stockfish:', error);
        return false;
    }
}

export function setPosition(fen) {
    if (stockfish && isReady) {
        stockfish.postMessage(`position fen ${fen}`);
        return true;
    }
    return false;
}

export function setPositionFromMoves(moves) {
    if (stockfish && isReady) {
        const moveList = moves.join(' ');
        stockfish.postMessage(`position startpos moves ${moveList}`);
        return true;
    }
    return false;
}

export function go(depth = 15, moveTime = null) {
    if (stockfish && isReady) {
        if (moveTime) {
            stockfish.postMessage(`go movetime ${moveTime}`);
        } else {
            stockfish.postMessage(`go depth ${depth}`);
        }
        return true;
    }
    return false;
}

export function stop() {
    if (stockfish && isReady) {
        stockfish.postMessage('stop');
        return true;
    }
    return false;
}

export function setOption(name, value) {
    if (stockfish && isReady) {
        stockfish.postMessage(`setoption name ${name} value ${value}`);
        return true;
    }
    return false;
}

export function newGame() {
    if (stockfish && isReady) {
        stockfish.postMessage('ucinewgame');
        return true;
    }
    return false;
}

export function quit() {
    if (stockfish) {
        stockfish.postMessage('quit');
        stockfish = null;
        isReady = false;
    }
}


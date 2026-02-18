// Chess.js and Chessboard interop for Blazor
// This module provides a bridge between Blazor C# and chess libraries

let chess = null;
let board = null;
let dotNetHelper = null;

// Import chess.js from lib folder
const chessPromise = import('../lib/chess.js/chess.js');

export async function initializeChess(dotNetRef) {
    try {
        const { Chess } = await chessPromise;
        chess = new Chess();
        dotNetHelper = dotNetRef;
        console.log('Chess.js initialized');
        return true;
    } catch (error) {
        console.error('Failed to initialize chess.js:', error);
        return false;
    }
}

export function newGame() {
    if (chess) {
        chess.reset();
        return true;
    }
    return false;
}

export function loadFen(fen) {
    if (chess) {
        try {
            chess.load(fen);
            return true;
        } catch (error) {
            console.error('Failed to load FEN:', error);
            return false;
        }
    }
    return false;
}

export function getFen() {
    return chess ? chess.fen() : null;
}

export function getPgn() {
    return chess ? chess.pgn() : null;
}

export function makeMove(from, to, promotion = 'q') {
    if (!chess) return null;
    
    try {
        const move = chess.move({
            from: from,
            to: to,
            promotion: promotion
        });
        
        if (move) {
            return {
                from: move.from,
                to: move.to,
                piece: move.piece,
                captured: move.captured || null,
                promotion: move.promotion || null,
                san: move.san,
                lan: move.lan
            };
        }
        return null;
    } catch (error) {
        console.error('Invalid move:', error);
        return null;
    }
}

export function getLegalMoves(square = null) {
    if (!chess) return [];
    
    if (square) {
        return chess.moves({ square: square, verbose: true }).map(move => ({
            from: move.from,
            to: move.to,
            piece: move.piece,
            san: move.san
        }));
    }
    
    return chess.moves({ verbose: true }).map(move => ({
        from: move.from,
        to: move.to,
        piece: move.piece,
        san: move.san
    }));
}

export function isGameOver() {
    return chess ? chess.isGameOver() : false;
}

export function isCheck() {
    return chess ? chess.isCheck() : false;
}

export function isCheckmate() {
    return chess ? chess.isCheckmate() : false;
}

export function isDraw() {
    return chess ? chess.isDraw() : false;
}

export function getTurn() {
    return chess ? chess.turn() : 'w';
}

export function undoMove() {
    if (chess) {
        const move = chess.undo();
        return move !== null;
    }
    return false;
}

export function getHistory() {
    return chess ? chess.history() : [];
}

export function getBoardPosition() {
    if (!chess) return {};
    
    const board = chess.board();
    const position = {};
    
    for (let row = 0; row < 8; row++) {
        for (let col = 0; col < 8; col++) {
            const piece = board[row][col];
            if (piece) {
                const square = String.fromCharCode(97 + col) + (8 - row);
                position[square] = piece.color + piece.type.toUpperCase();
            }
        }
    }
    
    return position;
}


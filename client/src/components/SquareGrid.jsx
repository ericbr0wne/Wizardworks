import React, { useState, useEffect } from 'react';
import { squareService } from '../services/squareService';

export const SquareGrid = () => {
    const [squares, setSquares] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
        loadSquares();
    }, []);

    const loadSquares = async () => {
        try {
            setIsLoading(true);
            const data = await squareService.getSquares();
            setSquares(data);
        } catch (err) {
            setError('Failed to load squares');
        } finally {
            setIsLoading(false);
        }
    };

    const handleAddSquare = async () => {
        try {
            setIsLoading(true);
            const newSquare = await squareService.addSquare();
            setSquares([...squares, newSquare]);
        } catch (err) {
            setError('Failed to add square');
        } finally {
            setIsLoading(false);
        }
    };

    const maxX = Math.max(...squares.map(s => s.position.x), 0);
    const maxY = Math.max(...squares.map(s => s.position.y), 0);
    const gridSize = Math.max(maxX, maxY) + 1;

    return (
        <div className="min-h-screen bg-[#1A1625] flex flex-col items-center gap-6 p-8">
            <h1 className="text-2xl font-bold text-white">Wizardworks: Programmeringsuppgift</h1>

            {error && (
                <div className="text-red-500 mb-4">{error}</div>
            )}

            <button
                onClick={handleAddSquare}
                disabled={isLoading}
                className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600 transition-colors disabled:opacity-50"
            >
                {isLoading ? 'Loading...' : 'Lägg till ruta'}
            </button>

            <div
                className="grid gap-2"
                style={{
                    gridTemplateColumns: `repeat(${gridSize}, minmax(0, 1fr))`,
                    width: `${gridSize * 5}rem`
                }}
            >
                {squares.map((square) => (
                    <div
                        key={square.id}
                        className="aspect-square rounded-lg shadow-md transition-all duration-300"
                        style={{
                            backgroundColor: square.color,
                            gridColumn: square.position.x + 1,
                            gridRow: square.position.y + 1
                        }}
                    />
                ))}
            </div>
        </div>
    );
};
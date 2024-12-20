import React, { useState, useEffect } from "react";
import {
  TextField,
  Button,
  Grid2,
  Box,
  Table,
  TableBody,
  TableCell,
  TableRow,
  Typography,
  MenuItem,
  Select,
} from "@mui/material";

function App() {
  const [n, setN] = useState(3);
  const [m, setM] = useState(3);
  const [p, setP] = useState(3);
  const [matrix, setMatrix] = useState([]);
  const [responseMessageResult, setResponseMessageResult] = useState("");
  const [responseMessageDatabase, setResponseMessageDatabase] = useState("");
  const [validationError, setValidationError] = useState("");
  const [history, setHistory] = useState([]);
  const [selectedHistory, setSelectedHistory] = useState("");

  const CELL_SIZE = 70;

  useEffect(() => {
    updateMatrix();
  }, [n, m, p]);

  const fetchHistory = async () => {
    try {
      const response = await fetch("http://localhost:7284/main/get-history");
      const data = await response.json();
      if (data.succeed) {
        const sortedHistory = data.data.sort(
          (a, b) => new Date(b.time) - new Date(a.time)
        );
        setHistory(sortedHistory);
      }
    } catch (error) {
      console.error("Error fetching history:", error);
    }
  };

  const handleHistoryChange = (event) => {
    const selectedId = event.target.value;
    setSelectedHistory(selectedId);
    const selectedItem = history.find((item) => item.id === selectedId);

    if (selectedItem) {
      setN(selectedItem.n);
      setM(selectedItem.m);
      setP(selectedItem.p);
      setMatrix(JSON.parse(selectedItem.matrix));
    }
  };

  const updateMatrix = (newN = n, newM = m, newP = p) => {
    const updatedMatrix = Array.from({ length: newN }, (_, rowIndex) =>
      Array.from({ length: newM }, (_, colIndex) => {
        if (matrix[rowIndex]?.[colIndex] !== undefined) {
          return matrix[rowIndex][colIndex] === ""
            ? ""
            : Math.min(matrix[rowIndex][colIndex], newP);
        } else {
          return "";
        }
      })
    );
    setMatrix(updatedMatrix);
  };

  const randomizeMatrix = () => {
    const requiredNumbers = Array.from({ length: p }, (_, i) => i + 1);
    const remainingCells = n * m - p;
    const randomNumbers = Array.from(
      { length: remainingCells },
      () => Math.floor(Math.random() * p) + 1
    );
    const allNumbers = [...requiredNumbers, ...randomNumbers];
    for (let i = allNumbers.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [allNumbers[i], allNumbers[j]] = [allNumbers[j], allNumbers[i]];
    }
    let index = 0;
    const updatedMatrix = Array.from({ length: n }, () =>
      Array.from({ length: m }, () => {
        const value = allNumbers[index];
        index += 1;
        return value;
      })
    );
    setMatrix(updatedMatrix);
  };

  const handleInputChange = (setter, max) => (event) => {
    const value = Math.max(1, Math.min(max, Number(event.target.value) || 1));
    setter(value);
  };

  const handlePChange = (event) => {
    const value = Math.max(1, Math.min(n * m, Number(event.target.value) || 1));
    setP(value);
  };

  const handleCellChange = (rowIndex, colIndex) => (event) => {
    const value = event.target.value;
    const numericValue = parseInt(value, 10);

    if (!isNaN(numericValue) && numericValue >= 1 && numericValue <= p) {
      const newMatrix = matrix.map((row, r) =>
        row.map((cell, c) =>
          r === rowIndex && c === colIndex ? numericValue : cell
        )
      );
      setMatrix(newMatrix);
    } else if (value === "") {
      const newMatrix = matrix.map((row, r) =>
        row.map((cell, c) => (r === rowIndex && c === colIndex ? "" : cell))
      );
      setMatrix(newMatrix);
    }
  };

  const validateMatrix = () => {
    for (const row of matrix) {
      for (const cell of row) {
        if (
          cell === "" ||
          cell === null ||
          cell === undefined ||
          parseInt(cell, 10) > p
        ) {
          return false;
        }
      }
    }
    return true;
  };

  const handleSubmit = async () => {
    setValidationError("");
    setResponseMessageResult("");

    if (!validateMatrix()) {
      setValidationError(
        "Please ensure all cells are filled before submitting."
      );
      return;
    }

    const matrixData = JSON.stringify({
      n: n.toString(),
      m: m.toString(),
      p: p.toString(),
      matrix: JSON.stringify(matrix),
    });

    try {
      const response = await fetch("http://localhost:7284/main/navigate", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: matrixData,
      });

      const data = await response.json();

      if (data.succeed) {
        setResponseMessageResult(data.data);
      } else {
        setResponseMessageResult("Error finding treasure.");
      }
    } catch (error) {
      setResponseMessageResult("Error finding treasure.");
    }

    try {
      const response = await fetch(
        "http://localhost:7284/main/save-to-database",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: matrixData,
        }
      );

      const data = await response.json();

      if (data.succeed) {
        setResponseMessageDatabase("Input saved to database");
      } else {
        setResponseMessageDatabase("Error saving input to database.");
      }
    } catch (error) {
      setResponseMessageDatabase("Error saving input to database.");
    }
  };

  return (
    <Box p={4}>
      <Grid2 container spacing={2} justifyContent="center">
        <Grid2>
          <Select
            value={selectedHistory}
            onChange={handleHistoryChange}
            onOpen={fetchHistory}
            displayEmpty
            style={{ minWidth: 200 }}
          >
            <MenuItem value="" disabled>
              Select Input History
            </MenuItem>
            {history.map((item) => (
              <MenuItem key={item.id} value={item.id}>
                {new Date(item.time).toLocaleString()}
              </MenuItem>
            ))}
          </Select>
        </Grid2>

        <Grid2>
          <TextField
            label="Rows (n)"
            type="number"
            value={n}
            onChange={(e) => handleInputChange(setN, 250)(e)}
            slotProps={{ min: 1, max: 250 }}
          />
        </Grid2>
        <Grid2>
          <TextField
            label="Columns (m)"
            type="number"
            value={m}
            onChange={(e) => handleInputChange(setM, 250)(e)}
            slotProps={{ min: 1, max: 250 }}
          />
        </Grid2>
        <Grid2>
          <TextField
            label="Treasure's number (p)"
            type="number"
            value={p}
            onChange={handlePChange}
          />
        </Grid2>
        <Grid2>
          <Button variant="contained" onClick={randomizeMatrix}>
            Randomize Map
          </Button>
        </Grid2>
        <Grid2>
          <Button variant="contained" onClick={handleSubmit}>
            Find treasure
          </Button>
        </Grid2>
      </Grid2>

      <Box mt={4} display="flex" justifyContent="center">
        <Box
          style={{
            overflowX: "auto",
            overflowY: "auto",
            maxWidth: "100%",
            maxHeight: "450px",
            border: "1px solid #ddd",
          }}
        >
          <Table
            style={{
              borderCollapse: "collapse",
              margin: "auto",
              width: `${m * CELL_SIZE}px`,
            }}
          >
            <TableBody>
              {matrix.map((row, rowIndex) => (
                <TableRow key={rowIndex}>
                  {row.map((value, colIndex) => (
                    <TableCell
                      key={`${rowIndex}-${colIndex}`}
                      style={{
                        border: "1px solid #ddd",
                        width: `${CELL_SIZE}px`,
                        height: `${CELL_SIZE}px`,
                        textAlign: "center",
                        verticalAlign: "middle",
                        backgroundColor: "#f9f9f9",
                        fontSize: "14px",
                        padding: 0,
                      }}
                    >
                      <input
                        type="text"
                        inputMode="numeric"
                        value={value}
                        onChange={handleCellChange(rowIndex, colIndex)}
                        maxLength={String(p).length}
                        style={{
                          width: "100%",
                          height: "100%",
                          border: "none",
                          textAlign: "center",
                          background: "transparent",
                          outline: "none",
                        }}
                      />
                    </TableCell>
                  ))}
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </Box>
      </Box>

      {validationError && (
        <Box mt={2} display="flex" justifyContent="center">
          <Typography variant="h6" color="error">
            {validationError}
          </Typography>
        </Box>
      )}

      {responseMessageResult && (
        <Box mt={2} display="flex" justifyContent="center">
          <Typography variant="h6" color="primary">
            {responseMessageResult}
          </Typography>
        </Box>
      )}

      {responseMessageDatabase && (
        <Box mt={2} display="flex" justifyContent="center">
          <Typography variant="h6" color="primary">
            {responseMessageDatabase}
          </Typography>
        </Box>
      )}
    </Box>
  );
}

export default App;

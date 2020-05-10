# Sudoku Solver

Solving sudoku puzzles is good for the brain - right? 
But actually solving them can be so hard.

This API implements a brute-force backtracking algorithm for solving the typical 
9x9 sudoku puzzle. 

Submit a puzzle with a request that looks like

```
POST /api/solve HTTP/1.1
Host: your-host
Content-Type: text/plain

090008604
600019000
048625000
020080716
070000080
385070020
000861540
000350002
507200030
```

the response will also be text/plan and contain the solved puzzle.


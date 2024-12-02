package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	scanner := bufio.NewScanner(os.Stdin)
	safe, unsafe := 0, 0

	fmt.Println("Enter pairs of numbers, one pair per line.")
	fmt.Println("Empty line finishes the input.")

	for {
		scanner.Scan()
		line := scanner.Text()
		if strings.TrimSpace(line) == "" {
			break
		}

		row := parseInput(line)
		if isSafe(row) {
			safe++
		} else {
			unsafe++
		}
	}

	fmt.Println("Safe:", safe)
	fmt.Println("Unsafe:", unsafe)
}

func parseInput(input string) []int {
	parts := strings.Fields(input)
	numbers := make([]int, len(parts))
	for i, part := range parts {
		num, err := strconv.Atoi(part)
		if err != nil {
			fmt.Println("Invalid input:", part)
			os.Exit(1)
		}
		numbers[i] = num
	}
	return numbers
}

func isSafe(row []int) bool {
	isIncreasing := false
	isDecreasing := false

	for i := 1; i < len(row); i++ {
		if row[i-1]-row[i] > 3  || row[i] - row[i-1] > 3 || row[i] - row[i-1] == 0 {
			return false 
		}

		if row[i] < row[i-1] {
			isDecreasing = true
		} else if row[i] > row[i-1] {
			isIncreasing = true
		}

		if isDecreasing && isIncreasing {
			return false
		}
	}

	return true
}

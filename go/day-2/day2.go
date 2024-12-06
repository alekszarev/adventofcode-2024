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
	lineNumber := 0

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
			fmt.Println("Safe:", lineNumber)
			safe++
		} else {
			fmt.Println("Unsafe:", lineNumber)
			unsafe++
		}

		lineNumber++
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

	badLevelCounter := 0
	badLevelIndex := -1
	badLevelCheck := false

	for i := 1; i < len(row); i++ {
		index := i - 1

		if badLevelCounter == 1 && !badLevelCheck {
			index = badLevelIndex
			badLevelCheck = true
		}

		if row[index]-row[i] > 3 || row[i]-row[index] > 3 {
			return false
		}

		if row[i]-row[index] == 0 {
			badLevelCounter++
			badLevelIndex = i
		} else if row[i] < row[index] {
			isDecreasing = true
			if isIncreasing {
				badLevelCounter++
				badLevelIndex = i
			}
		} else if row[i] > row[index] {
			isIncreasing = true
			if isDecreasing {
				badLevelCounter++
				badLevelIndex = i
			}
		}

		if badLevelCounter > 1 {
			return false
		}

		if badLevelCounter == 1 && isIncreasing && isDecreasing {
			isDecreasing = false
			isIncreasing = false
		}
	}

	return true
}

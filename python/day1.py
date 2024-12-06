from collections import Counter

def main():
    print("Enter pairs of numbers, one pair per line.")
    print("Empty line finishes the input.")
    
    first = [] 
    second = [] 

    while True:
        line = input()      
        if not line.strip():
            break
        
        try:
            num1, num2 = map(int, line.split())
            first.append(num1)
            second.append(num2)
        except ValueError:
            print("Invalid input")
    
    first.sort() 
    second.sort() 

    partOneResult = 0
    for num1, num2 in zip(first, second):
        diff = abs(num2 - num1)
        partOneResult += diff

    print(f"Part one result: {partOneResult}")

    #part two

    second_counter = Counter(second)
    partTwoResult = 0

    for num in first:
        partTwoResult += num * second_counter[num]

    print(f"Part two result: {partTwoResult}")

if __name__ == "__main__":
    main()

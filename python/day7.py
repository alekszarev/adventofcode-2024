from typing import List

def main():
    print("Enter pairs of equations, one pair per line.")
    print("Empty line finishes the input.")

    sum = 0 
    while True:
        line = input()
        if not line:
            break
        
        try:
            key_part, value_part = line.split(":")
            key = int(key_part.strip())
            values = list(map(int, value_part.strip().split()))

            resultOfEquation = equationApplied(key, values)
            print(f"result: {resultOfEquation}")
            sum += resultOfEquation
        except ValueError:
            print("Invalid input")

    print(f"Sum: {sum}")

def equationApplied(result: int, numbers: List[int]) -> int:
    def equation(current_result: int, remaining: List[int]) -> int:
       if not remaining:
           return current_result if current_result == result else 0
       
       first, rest = remaining[0], remaining[1:]
       
       add_result = equation(current_result + first, rest)
       if add_result == result:
           return add_result
       
       multiply_result = equation(current_result * first, rest)
       if multiply_result == result:
           return multiply_result
       
       multiplier = 10 ** len(str(first))
       concat_result = equation(current_result * multiplier + first, rest)
       if concat_result == result:
           return concat_result

       return 0
    return equation(numbers[0], numbers[1:])

def multiply(a: int, b: int) -> int:
    return a * b

def sum(a: int, b: int) -> int:
    return a + b

if __name__ == "__main__":
    main()
    
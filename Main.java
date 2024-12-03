import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Main {

    public static void main(String[] args) {
        try (Scanner scanner = new Scanner(System.in)) {
          System.out.println("Input:");
          System.out.println("Empty line finishes the input");
          
          StringBuilder input = new StringBuilder();
          while (true) {
            String line = scanner.nextLine();
            if (line.trim().isEmpty()) {
                break;
            }
            input.append(line);
          }

          Pattern pattern = Pattern.compile("mul\\((\\d{1,3}),\\s?(\\d{1,3})\\)");
          Matcher matcher = pattern.matcher(input);

          int result = 0;
          while (matcher.find()) {
            int value1 = Integer.parseInt(matcher.group(1));
            int value2 = Integer.parseInt(matcher.group(2));
            result += value1 * value2;
          }

          System.out.println("Result: " + result);
        }
    }
}
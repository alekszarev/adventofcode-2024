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
          int result = 0;

          //part two
          
          int doIndex = input.indexOf("do()");
          String beforeDoSection = (doIndex != -1) ? input.substring(0, doIndex) : input.toString();
          Matcher beforeDoMatcher = pattern.matcher(beforeDoSection);

          while (beforeDoMatcher.find()) {
              int value1 = Integer.parseInt(beforeDoMatcher.group(1));
              int value2 = Integer.parseInt(beforeDoMatcher.group(2));
              result += value1 * value2;
          }
   
          Pattern doToDontPattern =  Pattern.compile("do\\(\\).*?don't\\(\\)");
          Matcher doToDontMatcher = doToDontPattern.matcher(input);
          while (doToDontMatcher.find()) {
            String doToDontSection = doToDontMatcher.group();
            Matcher sectionMatcher = pattern.matcher(doToDontSection);

            while (sectionMatcher.find()) {
                int value1 = Integer.parseInt(sectionMatcher.group(1));
                int value2 = Integer.parseInt(sectionMatcher.group(2));
                result += value1 * value2;
            }
          }

          System.out.println("Result: " + result);
        }
    }
}
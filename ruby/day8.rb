require 'set'

puts "Enter input. Empty line finishes matrix:"
grid = []
while (line = gets.chomp) && !line.empty?
  grid << line.chars
end

rows = grid.size
cols = grid[0].size
unique_antidotes = Set.new

def find_antennas(grid)
  antennas = Hash.new { |dict, key| dict[key] = [] }
  grid.each_with_index do |row, i|
    row.each_with_index do |cell, j|
      antennas[cell] << [i, j] if cell.match?(/[a-zA-Z0-9]/)
    end
  end
  antennas
end

antennas = find_antennas(grid)

antennas.each do |freq, locations|
  locations.combination(2) do |(r1, c1), (r2, c2)|
    row = r2 - r1
    col_diff = c2 - c1

    antinode1 = [r1 - row, c1 - col_diff]
    antinode2 = [r2 + row, c2 + col_diff]

    unique_antidotes.add(antinode1) if antinode1[0] >= 0 && antinode1[0] < rows && antinode1[1] >= 0 && antinode1[1] < cols
    unique_antidotes.add(antinode2) if antinode2[0] >= 0 && antinode2[0] < rows && antinode2[1] >= 0 && antinode2[1] < cols
  end
end

output_grid = Array.new(rows) { Array.new(cols, '.') }
unique_antidotes.each do |r, c|
  if r >= 0 && r < rows && c >= 0 && c < cols
    output_grid[r][c] = '#'
  end
end

grid.each_with_index do |row, r|
  row.each_with_index do |cell, c|
    output_grid[r][c] = cell unless cell == '.'
  end
end

puts "Modified Grid:"
output_grid.each { |row| puts row.join }
puts "Antidotes: #{unique_antidotes.size}"

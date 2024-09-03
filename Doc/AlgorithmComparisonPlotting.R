# Ensure the necessary packages are loaded
library(ggplot2)
library(dplyr)
library(reshape2)

data <- read.csv("DIRECTORY PATH HERE")

# Step 2: Convert relevant columns to numeric, coercing non-numeric values to NA
data$JPS.time <- as.numeric(as.character(data$JPS.time))
data$Dijkstra.time <- as.numeric(as.character(data$Dijkstra.time))
data$A..time <- as.numeric(as.character(data$A..time))
data$JPS.shortest.path.length <- as.numeric(as.character(data$JPS.shortest.path.length))
data$Dijkstra.shortest.path.length <- as.numeric(as.character(data$Dijkstra.shortest.path.length))
data$A..shortest.path.length <- as.numeric(as.character(data$A..shortest.path.length))

# Remove rows with NA values, which may have been introduced by coercion
cleaned_data <- na.omit(data)

# Step 3: Select and rename the relevant columns
selected_data <- cleaned_data %>%
  select(JPS_time = `JPS.time`, 
         Dijkstra_time = `Dijkstra.time`, 
         A_star_time = `A..time`, 
         JPS_length = `JPS.shortest.path.length`, 
         Dijkstra_length = `Dijkstra.shortest.path.length`, 
         A_star_length = `A..shortest.path.length`)

# Filter the data to remove rows with path lengths under 5
filtered_data <- selected_data %>%
  filter(JPS_length >= 1 & Dijkstra_length >= 1 & A_star_length >= 1)

# Combine the times and lengths into a single dataframe
combined_data <- data.frame(
  Time = c(filtered_data$JPS_time, filtered_data$Dijkstra_time, filtered_data$A_star_time),
  Length = c(filtered_data$JPS_length, filtered_data$Dijkstra_length, filtered_data$A_star_length),
  Algorithm = rep(c("JPS", "Dijkstra", "A*"), each = nrow(filtered_data))
)

# Plot all three algorithms in one plot with Time on the y-axis and Length on the x-axis
ggplot(combined_data, aes(x = Length, y = Time, color = Algorithm)) +
  geom_point() +
  geom_smooth(method = "loess", se = FALSE) +
  labs(x = "Path Length", y = "Time (ms)", title = "Speed comparison (for MAP NAME HERE) map") +
  theme_minimal()


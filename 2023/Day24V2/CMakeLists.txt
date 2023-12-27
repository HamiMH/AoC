# CMakeList.txt : CMake project for Day24V2, include source and define
# project specific logic here.
#

# Add source to this project's executable.
add_executable (Day24V2 "Day24V2.cpp" "Day24V2.h")

if (CMAKE_VERSION VERSION_GREATER 3.12)
  set_property(TARGET Day24V2 PROPERTY CXX_STANDARD 20)
endif()

# TODO: Add tests and install targets if needed.

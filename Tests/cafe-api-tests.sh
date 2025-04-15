#!/bin/sh

############################################################################
# Setup
############################################################################

print_error() {
    printf "Error: $@\n" >&2
}

print_line() {
    printf "\n------------------------------------------------------------\n"
}

pretty_print() {
    printf "\033[1;33m"
    print_line
    printf "$@"
    print_line
    printf "\033[0m"
}

# Use httpie to send HTTP requests, but first print the request itself
request() {
    local method=$1
    shift
    local url=$1
    shift

    # Convert method to uppercase
    method=$(echo "$method" | tr '[:lower:]' '[:upper:]')

    printf "\n\033[1;36m>> %s %s\033[0m\n" "$method" "$url"

    http --body --check-status --ignore-stdin --print=HBhbm "$method" "$url" "$@"
    print_line
}

# Find project root
PROJECT_ROOT=$(git rev-parse --show-toplevel 2>/dev/null)
if [ -z "$PROJECT_ROOT" ]; then
    print_error "Not inside a Git repository.  Exiting."
    exit 1
fi

# Get variables (in this case, API_BASE_URL) from .env file
ENV_FILE="$PROJECT_ROOT/AdminClient/.env"
if [ -f "$ENV_FILE" ]; then
    . "$ENV_FILE"
else
    print_error "Missing .env file: $ENV_FILE.  Exiting."
    exit 1
fi

if [ -z "$API_BASE_URL" ]; then
    print_error "API base URL is missing from .env file.  Exiting."
    exit 1
fi

printf "\nLoaded API_BASE_URL: %s\n" "$API_BASE_URL"



############################################################################
# Admin API Requests
############################################################################
pretty_print "Admin Booking Management API requests"

# Get all bookings
request GET "$API_BASE_URL/admin/bookings"

# Get bookings by user ID
request GET "$API_BASE_URL/admin/bookings/user/1"

# Modify a booking (update description & table number)
request PUT "$API_BASE_URL/admin/bookings/42" \
    Description="Updated event details" \
    TableNumber:=7

# Cancel a booking
request DELETE "$API_BASE_URL/admin/bookings/42"

# # Update booking pricing -- not implemented!
# request PUT "$API_BASE_URL/admin/bookings/pricing" \
#     BookingId:=42 \
#     BasePrice:=100.0 \
#     Discount:=10.0 \
#     Tax:=8.0

############################################################################
pretty_print "Admin Order Processing API requests"

# Get all orders
request GET "$API_BASE_URL/admin/orders"

# Get orders for a specific user
request GET "$API_BASE_URL/admin/orders/user/1"

# Modify an order (e.g., update status to Ready)
request PUT "$API_BASE_URL/admin/orders/10" \
    Status="Ready"

# Delete an order
request DELETE "$API_BASE_URL/admin/orders/10"

############################################################################
pretty_print "Admin User Management API requests"

# Get all users
request GET "$API_BASE_URL/admin/users"

# Get a specific user
request GET "$API_BASE_URL/admin/users/1"

# Update user details
request PUT "$API_BASE_URL/admin/users/1" \
    Name="Alice Johnson" \
    Email="alice.j@example.com"

# Delete a user
request DELETE "$API_BASE_URL/admin/users/1"



############################################################################
# Admin API Requests
############################################################################
pretty_print "Customer Booking Management API requests"

# Create a new booking
request POST "$API_BASE_URL/bookings" \
    Description="Birthday Party" \
    TableNumber:=5 \
    EventTime="2025-04-20T18:00:00Z" \
    Guests:=15

# Get all bookings for the logged-in user
request GET "$API_BASE_URL/bookings/my"

# Retrieve a specific booking (replace with actual ID after creation)
request GET "$API_BASE_URL/bookings/1"

# Modify a booking (only if created by the user)
request PUT "$API_BASE_URL/bookings/1" \
    Description="Updated Party Details" \
    TableNumber:=6

# Cancel a booking (only if created by the user)
request DELETE "$API_BASE_URL/bookings/1"

# Invite guests to the booking (replace with actual booking ID)
request POST "$API_BASE_URL/bookings/1/invite" \
    Guests="guest1@example.com,guest2@example.com"

# Update guest RSVP for a specific booking and guest
request PUT "$API_BASE_URL/bookings/1/responses/1" \
    Response="Yes"

############################################################################
pretty_print "Customer Order Processing API requests"

# Get available menu items
request GET "$API_BASE_URL/menu"

# Add an item to the cart (replace with actual item details)
request POST "$API_BASE_URL/cart" \
    ItemId:=3 \
    Quantity:=2

# Get current cart details
request GET "$API_BASE_URL/cart"

# Remove an item from the cart (replace with actual item ID)
request DELETE "$API_BASE_URL/cart/3"

# Place an order (replace with actual cart details)
request POST "$API_BASE_URL/orders" \
    CartId:=1

# Get all orders for the logged-in user
request GET "$API_BASE_URL/orders/my"

# Get a specific order (replace with actual order ID)
request GET "$API_BASE_URL/orders/1"

# Update/Modify an order (before processing)
request PUT "$API_BASE_URL/orders/1" \
    Quantity:=3 \
    Status="Pending"

# Cancel an order (before processing)
request DELETE "$API_BASE_URL/orders/1"

# Start the secure payment process
request POST "$API_BASE_URL/checkout"

# Get real-time order status (replace with actual order ID)
request GET "$API_BASE_URL/orders/1/status"

############################################################################
pretty_print "Customer User Management API requests"

# Register a new user
request POST "$API_BASE_URL/register" \
    Name="John Doe" \
    Email="john.doe@example.com" \
    Password="badpassword"

# Login and receive authentication token (replace with actual login details)
request POST "$API_BASE_URL/login" \
    Email="john.doe@example.com" \
    Password="badpassword"

# Logout user
request POST "$API_BASE_URL/logout"

# Send password reset link
request POST "$API_BASE_URL/reset-password" \
    Email="john.doe@example.com"

# Retrieve current user profile
request GET "$API_BASE_URL/users/my"

# Update current user details
request PUT "$API_BASE_URL/users/my" \
    Name="Max Power" \
    Email="maxpower@example.com"

# Deactivate own account
request DELETE "$API_BASE_URL/users/my"




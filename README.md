# Market Project

The **Market** project is a full-stack marketplace application connecting farmers/distributors with customers [1](#0-0) . It consists of a .NET 9 backend API (`MarketAPI`) and a React + TypeScript frontend (`MarketClient`) [2](#0-1) .

## Architecture

### Backend (MarketAPI)
The backend is built with **ASP.NET Core 9.0** and uses **Entity Framework Core** with **PostgreSQL** as the database [3](#0-2) [4](#0-3) . Key features include:

- **JWT Authentication**: Implements token-based authentication with refresh tokens [5](#0-4) [6](#0-5) 
- **Role-Based Access Control**: Three user roles (Admin, Customer, Supplier) with a many-to-many relationship [7](#0-6) 
- **Swagger Documentation**: Auto-generated API documentation [8](#0-7) 
- **Logging**: NLog integration for application logging [9](#0-8) 
- **Docker Support**: Containerized deployment with Linux target [10](#0-9) 

### Frontend (MarketClient)
The frontend uses **React + TypeScript + Vite** with modern tooling [11](#0-10) :

- **Fast Refresh**: HMR support via Vite plugins [12](#0-11) 
- **RTK Query**: API integration using Redux Toolkit Query [13](#0-12) 
- **TypeScript**: Type-safe development with strict linting options available [14](#0-13) 

## Core Features

### Product Catalog
- **Categories**: Six predefined categories (Frutas y Verduras, Lácteos y Huevos, Granos y Cereales, Carnes y Embutidos, Panadería y Desayuno, Otros) [15](#0-14) 
- **Products**: Individual items with measurement units and categories [16](#0-15) 
- **Combos**: Product bundles with fixed pricing [17](#0-16) 
- **Measurement Units**: Nine units including weight (g, kg, lb) and volume/count (ml, l, un, doc, pq, cj) [18](#0-17) 

### User Management
- **User Accounts**: Registration with profile information, phone, and farmer/distributor flag [19](#0-18) 
- **Supplier Profiles**: Extended profiles with business details, location, social media links, and blog content [20](#0-19) 
- **Reviews**: Customer ratings and reviews for suppliers [21](#0-20) 

### Order Processing
- **Orders**: Customer purchases with status tracking, payment info, and shipping address [22](#0-21) 
- **Order Items**: Support for both individual products and combo packages [23](#0-22) 
- **Payments**: Payment gateway integration with provider tracking [24](#0-23) 

### Additional Features
- **Calendar & Events**: Event management system [25](#0-24) 
- **Catalog API**: Public endpoints for browsing active products and combos [26](#0-25) 

## Technology Stack

**Backend:**
- .NET 9.0
- Entity Framework Core 9.0.7
- PostgreSQL (via Npgsql 9.0.4)
- JWT Bearer Authentication
- NLog 6.0.1
- Swashbuckle (Swagger) 9.0.3

**Frontend:**
- React 18+
- TypeScript
- Vite
- Redux Toolkit Query
- ESLint

## Notes

The project follows a clean architecture with Entity Framework migrations for database schema management<cite />. The database uses auto-incrementing integer primary keys and PostgreSQL's `now()` function for automatic timestamp management<cite />. The frontend is configured with Vite for fast development and supports both Babel and SWC for Fast Refresh [12](#0-11) . The codebase includes comprehensive ESLint configuration options for production applications [14](#0-13) .

Wiki pages you might want to explore:
- [Core Entities (leoCortes123/Market)](/wiki/leoCortes123/Market#3.2.1)

### Citations

**File:** MarketAPI/MarketAPI/Data/MarketDbContext.cs (L19-38)
```csharp
    // DbSets
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<MeasurementUnit> MeasurementUnits { get; set; } = null!;
    public DbSet<Combo> Combos { get; set; } = null!;
    public DbSet<ComboProduct> ComboProducts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    public DbSet<UserUserRole> UserUserRoles { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<SupplierProduct> SupplierProducts { get; set; } = null!;
    public DbSet<SupplierCombo> SupplierCombos { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<OrderComboItem> OrderComboItems { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<SupplierReview> SupplierReviews { get; set; } = null!;
    public DbSet<Calendar> Calendars { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
```

**File:** MarketClient/README.md (L1-8)
```markdown
# React + TypeScript + Vite

This template provides a minimal setup to get React working in Vite with HMR and some ESLint rules.

Currently, two official plugins are available:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react) uses [Babel](https://babeljs.io/) for Fast Refresh
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react-swc) uses [SWC](https://swc.rs/) for Fast Refresh
```

**File:** MarketClient/README.md (L10-40)
```markdown
## Expanding the ESLint configuration

If you are developing a production application, we recommend updating the configuration to enable type-aware lint rules:

```js
export default tseslint.config([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Other configs...

      // Remove tseslint.configs.recommended and replace with this
      ...tseslint.configs.recommendedTypeChecked,
      // Alternatively, use this for stricter rules
      ...tseslint.configs.strictTypeChecked,
      // Optionally, add this for stylistic rules
      ...tseslint.configs.stylisticTypeChecked,

      // Other configs...
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // other options...
    },
  },
])
```
```

**File:** MarketAPI/MarketAPI/MarketAPI.csproj (L4-4)
```text
    <TargetFramework>net9.0</TargetFramework>
```

**File:** MarketAPI/MarketAPI/MarketAPI.csproj (L10-10)
```text
	  <DockerDefaultTargetOS>Linux</DockerDefaultTargetOS>
```

**File:** MarketAPI/MarketAPI/MarketAPI.csproj (L14-14)
```text
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.7" />
```

**File:** MarketAPI/MarketAPI/MarketAPI.csproj (L25-26)
```text
    <PackageReference Include="NLog" Version="6.0.1" />
    <PackageReference Include="NLog.Web.AspNetCore" Version="6.0.1" />
```

**File:** MarketAPI/MarketAPI/MarketAPI.csproj (L27-27)
```text
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.4" />
```

**File:** MarketAPI/MarketAPI/MarketAPI.csproj (L28-28)
```text
    <PackageReference Include="Swashbuckle.AspNetCore" Version="9.0.3" />
```

**File:** MarketClient/src/store/api/catalogApi.ts (L5-24)
```typescript
export const catalogApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    // Obtener productos activos
    getActiveProducts: builder.query<Product[], void>({
      query: () => '/catalog/active-products',
      providesTags: ['Product'],
    }),

    // Obtener combos activos
    getActiveCombos: builder.query<Combo[], void>({
      query: () => '/catalog/active-combos',
      providesTags: ['Combo'],
    }),

    // Obtener productos por categoría (desde catálogo)
    getCatalogProductsByCategory: builder.query<Product[], number>({
      query: (categoryId) => `/catalog/category/${categoryId}/products`,
      providesTags: ['Product'],
    }),
  }),
```

**File:** MarketAPI/MarketAPI/Migrations/20250930040449_InitialCreate.cs (L498-509)
```csharp
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Productos frescos del campo", "Frutas y Verduras" },
                    { 2, "Productos lácteos y huevos frescos", "Lácteos y Huevos" },
                    { 3, "Arroz, frijol, maíz y más", "Granos y Cereales" },
                    { 4, "Carnes frescas y procesadas", "Carnes y Embutidos" },
                    { 5, "Arepas, pan, café y más", "Panadería y Desayuno" },
                    { 6, "Productos varios", "Otros" }
                });
```

**File:** MarketAPI/MarketAPI/Migrations/20250930040449_InitialCreate.cs (L511-514)
```csharp
            migrationBuilder.InsertData(
                table: "Combos",
                columns: new[] { "Id", "Description", "ImageUrl", "IsActive", "Name", "Price" },
                values: new object[] { 1, "Incluye arepas, queso y café", "desayuno.jpg", true, "Desayuno Campesino", 25000m });
```

**File:** MarketAPI/MarketAPI/Migrations/20250930040449_InitialCreate.cs (L516-530)
```csharp
            migrationBuilder.InsertData(
                table: "MeasurementUnits",
                columns: new[] { "Id", "Abbreviation", "IsWeight", "Name", "WeightInGrams" },
                values: new object[,]
                {
                    { 1, "g", true, "Gramo", 1 },
                    { 2, "kg", true, "Kilogramo", 1000 },
                    { 3, "lb", true, "Libra", 453 },
                    { 4, "ml", false, "Mililitro", null },
                    { 5, "l", false, "Litro", null },
                    { 6, "un", false, "Unidad", null },
                    { 7, "doc", false, "Docena", null },
                    { 8, "pq", false, "Paquete", null },
                    { 9, "cj", false, "Caja", null }
                });
```

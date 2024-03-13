function Footer() {
  const Year = new Date().getFullYear();

  return (
    <footer className="bg-dark text-center text-lg-start">
      <div className="col">© {Year} Copyright : ABW LLC</div>
    </footer>
  );
}

export default Footer;

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="add_general_information.xslt" />
  <xsl:output method="html" indent="no" />

  <xsl:template match="r">
    <xsl:call-template name="addGeneralInformation">
      <xsl:with-param name="isMember" select="false()" />
    </xsl:call-template>
  </xsl:template>
</xsl:stylesheet>
